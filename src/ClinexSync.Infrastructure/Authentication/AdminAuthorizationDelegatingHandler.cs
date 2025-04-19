using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ClinexSync.Contracts.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ClinexSync.Infrastructure.Authentication;

internal sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly string _cacheKey = "KeycloakToken";
    private readonly KeycloakOptions _keycloakOptions;
    private readonly IMemoryCache _cache;

    public AdminAuthorizationDelegatingHandler(
        IOptions<KeycloakOptions> keycloakOptions,
        IMemoryCache cache
    )
    {
        _keycloakOptions = keycloakOptions.Value;
        _cache = cache;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        await SetAccessToken(request, cancellationToken);

        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);

        if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
        {
            _cache.Remove(_cacheKey);
            await SetAccessToken(request, cancellationToken);
            httpResponseMessage = await base.SendAsync(request, cancellationToken);
        }

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task SetAccessToken(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        AuthorizationToken authorizationToken = await GetAuthorizationTokenAsync(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            authorizationToken.AccessToken
        );
    }

    private async Task<AuthorizationToken> GetAuthorizationTokenAsync(
        CancellationToken cancellationToken
    )
    {
        if (_cache.TryGetValue(_cacheKey, out AuthorizationToken authorizationToken))
        {
            return authorizationToken;
        }

        var authorizationRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keycloakOptions.ClientId),
            new("client_secret", _keycloakOptions.ClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials"),
        };

        var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

        using var authorizationRequest = new HttpRequestMessage(
            HttpMethod.Post,
            new Uri(_keycloakOptions.BaseUrl + "/protocol/openid-connect/token")
        )
        {
            Content = authorizationRequestContent,
        };

        HttpResponseMessage authorizationResponse = await base.SendAsync(
            authorizationRequest,
            cancellationToken
        );

        authorizationResponse.EnsureSuccessStatusCode();

        AuthorizationToken token =
            await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(
                cancellationToken
            ) ?? throw new ApplicationException();

        _cache.Set(_cacheKey, token);

        return token;
    }
}

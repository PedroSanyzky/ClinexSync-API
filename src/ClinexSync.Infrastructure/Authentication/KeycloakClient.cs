using System.Net.Http.Json;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;
using Microsoft.Extensions.Options;

namespace ClinexSync.Infrastructure.Authentication;

public sealed class KeycloakClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly KeycloakOptions _keycloakOptions;

    public KeycloakClient(
        IHttpClientFactory httpClientFactory,
        IOptions<KeycloakOptions> keycloakConfig
    )
    {
        _httpClientFactory = httpClientFactory;
        _keycloakOptions = keycloakConfig.Value;
    }

    public async Task<Result<AuthorizationToken>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.ClientId),
                new("client_secret", _keycloakOptions.ClientSecret),
                new("scope", "openid"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password),
            };

            using var authorizationRequestContent = new FormUrlEncodedContent(
                authRequestParameters
            );

            HttpClient client = _httpClientFactory.CreateClient("KeycloakExternal");

            HttpResponseMessage response = await client.PostAsync(
                "protocol/openid-connect/token",
                authorizationRequestContent,
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            AuthorizationToken? authorizationToken =
                await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            if (authorizationToken is null)
            {
                return Result.Failure<AuthorizationToken>(KeycloakErrors.InvalidCredentials());
            }

            return authorizationToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<AuthorizationToken>(KeycloakErrors.InvalidCredentials());
        }
    }

    public async Task<Result<string>> RegisterAsync(
        Person user,
        string password,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials =
            [
                new()
                {
                    Value = password,
                    Temporary = false,
                    Type = "password",
                },
            ];

            HttpClient client = _httpClientFactory.CreateClient("KeycloakInternal");

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "users",
                userRepresentationModel,
                cancellationToken
            );

            string identityId = ExtractIdentityIdFromLocationHeader(response);

            if (string.IsNullOrEmpty(identityId))
            {
                throw new ApplicationException("Error ocurred to register new user.");
            }

            return Result.Success(identityId);
        }
        catch (HttpRequestException)
        {
            throw new ApplicationException("Error ocurred to register new user.");
        }
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage
    )
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase
        );

        string userIdentityId = locationHeader.Substring(
            userSegmentValueIndex + usersSegmentName.Length
        );

        return userIdentityId;
    }
}

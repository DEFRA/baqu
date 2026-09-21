using BAQU.Data;
using Microsoft.Graph.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BAQU.Repository;

public class PeopleRepository : IPeopleRepository
{
    private const string SAM_ACCOUNT_PATTERN = @"^(dt2\/)?[a-zA-Z]{2}\d{6}$";
    private const string SID_PATTERN = @"^([mM])\d{6,7}|(cx|CX)\d{5}$";
    
    private readonly string _apiBaseUrl;
    private readonly string _apiKey;
    private readonly PeopleContext _context;
    private readonly ILogger<PeopleRepository> _logger;
    private HttpClient _httpClient;

    // Properties for testing purposes
    public HttpClient HttpClientForTesting
    {
        set { _httpClient = value; }
    }
    
    // For testing - skip response state verification
    public bool SkipResponseStateVerification { get; set; } = false;

    public PeopleRepository(PeopleContext context, ILogger<PeopleRepository> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _httpClient = new HttpClient();
        _apiBaseUrl = configuration["PeopleApi:BaseUrl"] ?? throw new ArgumentNullException(nameof(configuration), "PeopleApi:BaseUrl is not configured");
        _apiKey = configuration["PeopleApi:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "PeopleApi:ApiKey is not configured");
    }

    public virtual async Task<User> Get(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            _logger.LogWarning("Get method called with null or empty id");
            return CreateEmptyUser();
        }

        try
        {
            var requestState = Guid.NewGuid().ToString();
            var request = CreateHttpRequest(id, requestState);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            if (!SkipResponseStateVerification && !VerifyResponseState(response, requestState))
            {
                _logger.LogCritical($"Request state mismatch. Request: {requestState}");
                throw new Exception("Dangerous response state detected");
            }

            return await ProcessResponse(response, id);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"HTTP request failed for id: {id}");
            return CreateEmptyUser();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"Failed to deserialise response for id: {id}");
            return CreateEmptyUser();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error when getting user with id: {id}");
            throw;
        }
    }

    private HttpRequestMessage CreateHttpRequest(string id, string requestState)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get
        };

        // Add headers
        request.Headers.Add("X-API-Key", _apiKey);
        request.Headers.Add("X-State", requestState);
        request.Headers.Add("X-GetManager", "true");

        request.RequestUri = DetermineRequestUri(id);

        return request;
    }

    private Uri DetermineRequestUri(string id)
    {
        // SAMAccount name match
        if (Regex.IsMatch(id, SAM_ACCOUNT_PATTERN))
        {
            return new Uri($"{_apiBaseUrl}/getusers/by-samaccount/{id}");
        }

        // SID name match
        if (Regex.IsMatch(id, SID_PATTERN))
        {
            return new Uri($"{_apiBaseUrl}/getusers/by-sid/{id}");
        }

        // Fallback to mailnickname if no other patterns match
        return new Uri($"{_apiBaseUrl}/getusers/by-mailnickname/{id}");
    }

    private bool VerifyResponseState(HttpResponseMessage response, string requestState)
    {
        response.Headers.TryGetValues("X-State", out var state);
        return state != null && state.First() == requestState;
    }

    private async Task<User> ProcessResponse(HttpResponseMessage response, string id)
    {
        var result = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var deserialisedResult = JsonSerializer.Deserialize<IEnumerable<User>>(result, options)?.FirstOrDefault();

        if (deserialisedResult == null)
        {
            _logger.LogWarning($"No user found for Id: {id}");
            return CreateEmptyUser();
        }

        if (string.IsNullOrEmpty(deserialisedResult.DisplayName))
        {
            _logger.LogWarning($"User found for Id: {id} but DisplayName is missing.");
            var manager = new User();
            manager.AdditionalData = new Dictionary<string, object> { { "DisplayName", "Unknown" } };
            return new User { DisplayName = id, Manager = manager };
        }

        return deserialisedResult;
    }

    private User CreateEmptyUser() => new User();

}

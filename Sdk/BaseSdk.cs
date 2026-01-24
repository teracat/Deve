using System.Text.Json;
using System.Text.Json.Serialization;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk;

internal class BaseSdk
{
    #region Properties
    protected ISdk Sdk { get; }

    public JsonSerializerOptions SerializerOptions { get; set; } = new()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        PropertyNameCaseInsensitive = true,
        PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
    };
    #endregion

    #region Constructor
    public BaseSdk(ISdk sdk)
    {
        Sdk = sdk;
    }
    #endregion

    #region Methods
    protected Task<ResultGetList<T>> GetList<T, RequestData>(string? path, RequestAuthType authType, RequestData? data) => GetList<T, RequestData>(path, authType, data, CancellationToken.None);

    protected async Task<ResultGetList<T>> GetList<T, RequestData>(string? path, RequestAuthType authType, RequestData? data, CancellationToken cancellationToken)
    {
        try
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Get, authType, Sdk.UserToken, data);
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.FailGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.FailGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGetList<T>>(content, SerializerOptions);
            return res ?? Result.FailGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.FailGetList<T>(ResultErrorType.Unknown, null, ex.Message);
        }
    }

    protected Task<ResultGet<T>> Get<T>(string? path, RequestAuthType authType) => Get<T>(path, authType, null, CancellationToken.None);

    protected Task<ResultGet<T>> Get<T>(string? path, RequestAuthType authType, Guid? id) => Get<T>(path, authType, id, CancellationToken.None);

    protected async Task<ResultGet<T>> Get<T>(string? path, RequestAuthType authType, Guid? id, CancellationToken cancellationToken)
    {
        try
        {
            var apiRequest = new RequestBuilder(path + (id?.ToString() ?? string.Empty), HttpMethod.Get, authType, Sdk.UserToken);
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGet<T>>(content, SerializerOptions);
            return res ?? Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.FailGet<T>(ResultErrorType.Unknown, null, ex.Message);
        }
    }

    protected Task<ResultGet<T>> Get<T, RequestData>(string? path, RequestAuthType authType, RequestData? data) => Get<T, RequestData>(path, authType, data, CancellationToken.None);

    protected async Task<ResultGet<T>> Get<T, RequestData>(string? path, RequestAuthType authType, RequestData? data, CancellationToken cancellationToken)
    {
        try
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Get, authType, Sdk.UserToken, data);
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGet<T>>(content, SerializerOptions);
            return res ?? Result.FailGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.FailGet<T>(ResultErrorType.Unknown, null, ex.Message);
        }
    }
    protected Task<Result> Post(string? path, RequestAuthType authType) => Post(path, authType, null, CancellationToken.None);

    protected Task<Result> Post(string? path, RequestAuthType authType, object? requestObj) => Post(path, authType, requestObj, CancellationToken.None);

    protected async Task<Result> Post(string? path, RequestAuthType authType, object? requestObj, CancellationToken cancellationToken)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected Task<ResultGet<ResultType>> PostWithResult<ResultType>(string? path, RequestAuthType authType) => PostWithResult<ResultType>(path, authType, null, CancellationToken.None);

    protected Task<ResultGet<ResultType>> PostWithResult<ResultType>(string? path, RequestAuthType authType, object? requestObj) => PostWithResult<ResultType>(path, authType, requestObj, CancellationToken.None);

    protected async Task<ResultGet<ResultType>> PostWithResult<ResultType>(string? path, RequestAuthType authType, object? requestObj, CancellationToken cancellationToken)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
        return await ExecuteWithResult<ResultType>(apiRequest, cancellationToken);
    }

    protected Task<Result> Put(string? path, RequestAuthType authType) => Put(path, authType, null, CancellationToken.None);

    protected Task<Result> Put(string? path, RequestAuthType authType, object? requestObj) => Put(path, authType, requestObj, CancellationToken.None);

    protected async Task<Result> Put(string? path, RequestAuthType authType, object? requestObj, CancellationToken cancellationToken)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Put, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected Task<Result> Patch(string? path, RequestAuthType authType) => Patch(path, authType, null, CancellationToken.None);

    protected Task<Result> Patch(string? path, RequestAuthType authType, object? requestObj) => Patch(path, authType, requestObj, CancellationToken.None);

    protected async Task<Result> Patch(string? path, RequestAuthType authType, object? requestObj, CancellationToken cancellationToken)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Patch, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected Task<Result> Delete(string? path, RequestAuthType authType) => Delete(path, authType, CancellationToken.None);

    protected async Task<Result> Delete(string? path, RequestAuthType authType, CancellationToken cancellationToken)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Delete, authType, Sdk.UserToken);
        return await Execute(apiRequest, cancellationToken);
    }

    private async Task<Result> Execute(RequestBuilder apiRequest, CancellationToken cancellationToken) => new(await ExecuteWithResult<object>(apiRequest, cancellationToken));

    private async Task<ResultGet<ResultType>> ExecuteWithResult<ResultType>(RequestBuilder apiRequest, CancellationToken cancellationToken)
    {
        try
        {
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.FailGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.FailGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGet<ResultType>>(content, SerializerOptions);
            return res ?? Result.FailGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.FailGet<ResultType>(ResultErrorType.Unknown, null, ex.Message);
        }
    }
    #endregion
}

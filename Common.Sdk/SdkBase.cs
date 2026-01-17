using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk;

internal class SdkBase<SdkType> where SdkType : ISdkCommon
{
    #region Properties
    protected SdkType Sdk { get; }

    public JsonSerializerOptions SerializerOptions { get; set; } = new()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        PropertyNameCaseInsensitive = true,
        PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
    };
    #endregion

    #region Constructor
    public SdkBase(SdkType sdk)
    {
        Sdk = sdk;
    }
    #endregion

    #region Methods
    protected async Task<ResultGetList<T>> GetList<T, RequestData>(string? path, RequestData? data, RequestAuthType authType, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Get, authType, Sdk.UserToken, data);
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.ErrorGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.ErrorGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGetList<T>>(content, SerializerOptions);
            return res ?? Result.ErrorGetList<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.ErrorGetList<T>(ResultErrorType.Unknown, null, ex.Message);
        }
    }

    protected async Task<ResultGet<T>> Get<T>(string? path, RequestAuthType authType, Guid? id = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiRequest = new RequestBuilder(path + (id?.ToString() ?? string.Empty), HttpMethod.Get, authType, Sdk.UserToken);
            using var request = apiRequest.Build();
            var response = await Sdk.Client.SendAsync(request, cancellationToken);
            if (response is null)
            {
                return Result.ErrorGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.ErrorGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGet<T>>(content, SerializerOptions);
            return res ?? Result.ErrorGet<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.ErrorGet<T>(ResultErrorType.Unknown, null, ex.Message);
        }
    }

    protected async Task<Result> Post(string? path, RequestAuthType authType, object? requestObj = null, CancellationToken cancellationToken = default)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected async Task<ResultGet<ResultType>> PostWithResult<ResultType>(string? path, RequestAuthType authType, object? requestObj = null, CancellationToken cancellationToken = default)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
        return await ExecuteWithResult<ResultType>(apiRequest, cancellationToken);
    }

    protected async Task<Result> Put(string? path, RequestAuthType authType, object? requestObj = null, CancellationToken cancellationToken = default)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Put, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected async Task<Result> Patch(string? path, RequestAuthType authType, object? requestObj = null, CancellationToken cancellationToken = default)
    {
        var apiRequest = new RequestBuilder(path, HttpMethod.Patch, authType, Sdk.UserToken, requestObj);
        return await Execute(apiRequest, cancellationToken);
    }

    protected async Task<Result> Delete(string? path, RequestAuthType authType, CancellationToken cancellationToken = default)
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
                return Result.ErrorGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.ErrorGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            var res = JsonSerializer.Deserialize<ResultGet<ResultType>>(content, SerializerOptions);
            return res ?? Result.ErrorGet<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.ErrorGet<ResultType>(ResultErrorType.Unknown, null, ex.Message);
        }
    }
    #endregion
}
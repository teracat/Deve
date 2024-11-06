using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deve.Sdk
{
    internal abstract class SdkBase<SdkType> where SdkType: ISdkCommon
    {
        #region Atributes
        private readonly SdkType _sdk;
        #endregion

        #region Properties
        protected SdkType Sdk => _sdk;
        protected abstract string Path { get; }
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
            _sdk = sdk;
        }
        #endregion

        #region Methods
        protected async Task<ResultGetList<T>> GetList<T, RequestData>(string? path, RequestData? data, RequestAuthType authType)
        {
            try
            {
                var apiRequest = new RequestBuilder(path, HttpMethod.Get, authType, Sdk.UserToken, data);
                using (var request = apiRequest.Build())
                {
                    var response = await _sdk.Client.SendAsync(request);
                    if (response is null)
                        return Utils.ResultGetListError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    if (!response.IsSuccessStatusCode)
                        return Utils.ResultGetListError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    string content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content))
                        return Utils.ResultGetListError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    var res = JsonSerializer.Deserialize<ResultGetList<T>>(content, SerializerOptions);
                    if (res is null)
                        return Utils.ResultGetListError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    return res;
                }
            }
            catch (Exception ex)
            {
                return Utils.ResultGetListError<T>(ResultErrorType.Unknown, null, ex.Message);
            }
        }

        protected async Task<ResultGet<T>> Get<T>(string? path, RequestAuthType authType, long? id = null)
        {
            try
            {
                var apiRequest = new RequestBuilder(path, HttpMethod.Get, authType, Sdk.UserToken);
                if (id.HasValue)
                    apiRequest.Add(nameof(CriteriaId.Id), id);
                using (var request = apiRequest.Build())
                {
                    var response = await _sdk.Client.SendAsync(request);
                    if (response is null)
                        return Utils.ResultGetError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    if (!response.IsSuccessStatusCode)
                        return Utils.ResultGetError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    string content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content))
                        return Utils.ResultGetError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    var res = JsonSerializer.Deserialize<ResultGet<T>>(content, SerializerOptions);
                    if (res is null)
                        return Utils.ResultGetError<T>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    return res;
                }
            }
            catch (Exception ex)
            {
                return Utils.ResultGetError<T>(ResultErrorType.Unknown, null, ex.Message);
            }
        }

        protected async Task<Result> Post(string? path, RequestAuthType authType, object? requestObj = null)
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
            return await Execute(apiRequest);
        }

        protected async Task<ResultGet<ResultType>> PostWithResult<ResultType>(string? path, RequestAuthType authType, object? requestObj = null)
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Post, authType, Sdk.UserToken, requestObj);
            return await ExecuteWithResult<ResultType>(apiRequest);
        }

        protected async Task<Result> Put(string? path, RequestAuthType authType, object? requestObj = null)
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Put, authType, Sdk.UserToken, requestObj);
            return await Execute(apiRequest);
        }

        protected async Task<Result> Delete(string? path, RequestAuthType authType, object? requestObj = null)
        {
            var apiRequest = new RequestBuilder(path, HttpMethod.Delete, authType, Sdk.UserToken, requestObj);
            return await Execute(apiRequest);
        }

        private async Task<Result> Execute(RequestBuilder apiRequest)
        {
            return new Result(await ExecuteWithResult<object>(apiRequest));
        }

        private async Task<ResultGet<ResultType>> ExecuteWithResult<ResultType>(RequestBuilder apiRequest)
        {
            try
            {
                using (var request = apiRequest.Build())
                {
                    var response = await _sdk.Client.SendAsync(request);
                    if (response is null)
                        return Utils.ResultGetError<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    if (!response.IsSuccessStatusCode)
                        return Utils.ResultGetError<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    string content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content))
                        return Utils.ResultGetError<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    var res = JsonSerializer.Deserialize<ResultGet<ResultType>>(content, SerializerOptions);
                    if (res is null)
                        return Utils.ResultGetError<ResultType>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                    return res;
                }
            }
            catch (Exception ex)
            {
                return Utils.ResultGetError<ResultType>(ResultErrorType.Unknown, null, ex.Message);
            }
        }
        #endregion
    }
}

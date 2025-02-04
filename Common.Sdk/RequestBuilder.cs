using System.Text;
using System.Text.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace Deve.Sdk
{
    internal class RequestBuilder
    {
        #region Fields
        private readonly NameValueCollection _collection = [];
        private readonly Dictionary<string, string> _headers = [];
        private readonly object? _value;
        private readonly UserToken? _userToken;
        #endregion

        #region Properties
        internal Dictionary<string, string> Headers =>  _headers;
        internal RequestAuthType AuthType { get; set; } = RequestAuthType.None;
        internal HttpMethod Method { get; set; } = HttpMethod.Get;
        internal string? Uri { get; set; }
        #endregion

        #region Constructors
        public RequestBuilder(string? uri, RequestAuthType authType, UserToken? userToken, object? val = null)
        {
            Uri = uri;
            AuthType = authType;
            _userToken = userToken;
            _value = val;
        }

        public RequestBuilder(string? uri, HttpMethod method, RequestAuthType authType, UserToken? userToken, object? val = null)
        {
            Uri = uri;
            Method = method;
            AuthType = authType;
            _userToken = userToken;
            _value = val;
        }
        #endregion

        #region Methods
        public RequestBuilder AddHeader(string name, string val)
        {
            _headers.Add(name, val);
            return this;
        }

        internal RequestBuilder Add(string name, ushort val)
        {
            _collection.Add(name, val.ToString());
            return this;
        }

        internal RequestBuilder Add(string name, ulong val)
        {
            _collection.Add(name, val.ToString());
            return this;
        }

        internal RequestBuilder Add(string name, bool obj)
        {
            _collection.Add(name, BoolToString(obj));
            return this;
        }

        internal RequestBuilder Add(string name, object? obj)
        {
            if (obj is not null)
                _collection.Add(name, Convert.ToString(obj));
            return this;
        }

        private string BoolToString(bool val)
        {
            return val ? "true" : "false";
        }

        public void Clear()
        {
            _collection.Clear();
        }

        private string ToQueryString()
        {
            if (_value is not null)
            {
                foreach (var p in _value.GetType().GetProperties())
                {
                    Add(p.Name, p.GetValue(_value));
                }
            }

            if (_collection.Count == 0)
                return string.Empty;

            return "?" + string.Join("&", _collection.AllKeys.Select(key => key + "=" + HttpUtility.UrlEncode(_collection[key])));
        }

        private string ToJson()
        {
            if (_value is not null)
                return JsonSerializer.Serialize(_value);
            else
                return JsonSerializer.Serialize(_collection);
        }

        public HttpRequestMessage Build()
        {
            var uri = Uri;
            if (Method == HttpMethod.Get)
            {
                uri += ToQueryString();
            }

            var request = new HttpRequestMessage(Method, uri);

            //Headers
            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Auth
            if (AuthType == RequestAuthType.Default && _userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(_userToken.Scheme, _userToken.Token);
            }

            //Content
            if (Method != HttpMethod.Get)
            {
                request.Content = new StringContent(ToJson(), Encoding.UTF8, "application/json");
            }

            return request;
        }
        #endregion
    }
}

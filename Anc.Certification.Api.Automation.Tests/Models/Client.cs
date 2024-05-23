using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Anc.Certification.Api.Automation.Tests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Anc.Certification.Api.Automation.Tests
{
    public interface IClient
    {
        string OcpApimSubscriptionKey { get; set; }

        /// <summary>
        /// Sends a GET request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        Task<ClientResponse> GetAsync(string url, Dictionary<string, string> urlParameters = null, string correlationId = null);

        /// <summary>
        /// Sends a GET request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        Task<ClientResponse<TResult>> GetAsync<TResult>(string url, Dictionary<string, string> urlParameters = null, string correlationId = null);

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        Task<ClientResponse> PostAsync(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        Task<ClientResponse<TResult>> PostAsync<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        /// <param name="file">The file to submit with the request</param>
        Task<ClientResponse<TResult>> PostAsync<TResult>(string url, Dictionary<string, string> body = null, IFormFile file = null, string correlationId = null);

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        Task<ClientResponse> PutAsync(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        Task<ClientResponse<TResult>> PutAsync<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        /// <param name="file">The file to submit with the request</param>
        Task<ClientResponse<TResult>> PutAsync<TResult>(string url, Dictionary<string, string> body = null, IFormFile file = null, string correlationId = null);

        /// <summary>
        /// Sends a DELETE request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        Task<ClientResponse> DeleteAsync(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a DELETE request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        Task<ClientResponse<TResult>> DeleteAsync<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a GET request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        ClientResponse Get(string url, Dictionary<string, string> urlParameters = null, string correlationId = null);

        /// <summary>
        /// Sends a GET request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        ClientResponse<TResult> Get<TResult>(string url, Dictionary<string, string> urlParameters = null, string correlationId = null);

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        ClientResponse Post(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        ClientResponse<TResult> Post<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        ClientResponse Put(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        ClientResponse<TResult> Put<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        ClientResponse Delete(string url, object formData = null, string correlationId = null);

        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        ClientResponse<TResult> Delete<TResult>(string url, object body = null, string correlationId = null);

        /// <summary>
        /// Sends a request
        /// </summary>
        /// <param name="configuration">The configuration of the request</param>
        ClientResponse<TResult> SendRequest<TResult>(ClientRequest configuration, string correlationId = null);

        /// <summary>
        /// This is the BaseUrl to use on all requests
        /// </summary>
        Uri ClientBaseUrl { get; set; }

        ClientCredentials ClientCredentials { get; }
    }

    /// <summary>
    /// This is an Http Client which inherits from ResSharp.RestClient.
    /// </summary>
    public sealed class Client : IClient
    {
        private ICacheService mCache;
        private IErrorLogger mErrorLogger;
        private ClientAuthenticationResponse mAuthResponse = null;

        #region " Constructors "

        /// <summary>
        /// Creates a new Rest client using the built in defaults for Caching, ErrorLogging, and Serializer.
        /// Uses InMemoryCache, Entities.Client.JsonSerializer, and TraceErrorLogger
        /// </summary>
        /// <param name="baseUrl">The base url of the remote service</param>
        /// <param name="authCredentials">The authentication credentials</param>
        /// <param name="usePollyRetry">The flag to use Polly retry</param>
        public Client(string baseUrl, ClientCredentials authCredentials, bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            InitializeCommonData(baseUrl: baseUrl, authCredentials: authCredentials, usePollyRetry: usePollyRetry, pollySetting: pollySetting);
        }

        /// <summary>
        /// Creates a new Rest client using the built in defaults for Caching, ErrorLogging, and Serializer. 
        /// Uses InMemoryCache, Entities.Client.JsonSerializer, and TraceErrorLogger
        /// </summary>
        /// <param name="baseUrl">The base url of the remote service</param>
        /// <param name="accessToken">The authentication token</param>
        /// <param name="ocpApimSubscriptionKey">The authentication credentials</param>
        /// <param name="usePollyRetry">The flag to use Polly retry</param>
        public Client(string baseUrl, string accessToken, string ocpApimSubscriptionKey, bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            InitializeCommonData(baseUrl: baseUrl, accessToken: accessToken, ocpApimSubscriptionKey: ocpApimSubscriptionKey, usePollyRetry: usePollyRetry, pollySetting: pollySetting);
        }

        /// <summary>
        /// Creates a new Rest client using the built in defaults for Caching, ErrorLogging, and Serializer.
        /// Uses InMemoryCache, Entities.Client.JsonSerializer, and TraceErrorLogger
        /// </summary>
        /// <param name="baseUrl">The base url of the remote service</param>
        /// <param name="authCredentials">The authentication credentials</param>
        /// <param name="ocpApimSubscriptionKey">The authentication credentials</param>
        /// <param name="usePollyRetry">The flag to use Polly retry</param>
        public Client(string baseUrl, ClientCredentials authCredentials, string ocpApimSubscriptionKey, bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            InitializeCommonData(baseUrl: baseUrl, authCredentials: authCredentials, ocpApimSubscriptionKey: ocpApimSubscriptionKey, usePollyRetry: usePollyRetry, pollySetting: pollySetting);
        }

        /// <summary>
        /// Creates a new Rest client using the built in defaults for Caching, ErrorLogging, and Serializer. 
        /// Uses InMemoryCache, Entities.Client.JsonSerializer, and TraceErrorLogger
        /// </summary>
        /// <param name="baseUrl">The base url of the remote service</param>
        /// <param name="accessToken">The authentication token</param>
        /// <param name="authCredentials">The authentication credentials</param>
        /// <param name="ocpApimSubscriptionKey">The authentication credentials</param>
        /// <param name="usePollyRetry">The flag to use Polly retry</param>
        public Client(string baseUrl, string accessToken, ClientCredentials authCredentials, string ocpApimSubscriptionKey, bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            InitializeCommonData(baseUrl: baseUrl, accessToken: accessToken, authCredentials: authCredentials, ocpApimSubscriptionKey: ocpApimSubscriptionKey, usePollyRetry: usePollyRetry, pollySetting: pollySetting);
        }

        /// <summary>
        /// Creates a new Rest client. Be sure to supply the required caching service as well as the serializer,
        /// error logger and base url for the target service
        /// </summary>
        /// <param name="cache">The ICacheService to use for caching</param>
        /// <param name="serializer">The JsonSerializer to use, you can use Entities.Client.JsonSerializer</param>
        /// <param name="errorLogger">The IErrorLogger to use in order to record exceptions</param>
        /// <param name="baseUrl">The base Url of the target service</param>
        /// <param name="authCredentials">The authentication credentials</param>
        /// <param name="usePollyRetry">The flag to use Polly retry</param>
        public Client(ICacheService cache, JsonSerializer serializer, IErrorLogger errorLogger, string baseUrl = null, ClientCredentials authCredentials = default, bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            InitializeCommonData(cache, serializer, errorLogger, baseUrl, authCredentials, usePollyRetry: usePollyRetry, pollySetting: pollySetting);
        }
        #endregion

        /// <summary>
        /// The flag to use Polly retry policy
        /// </summary>
        public bool UsePollyRetry { get; set; }

        /// <summary>
        /// The flag to use Polly retry policy
        /// </summary>
        public PollySetting PollySetting { get; set; }

        /// <summary>
        /// The APIM subscription key
        /// </summary>
        public string OcpApimSubscriptionKey { get; set; }

        /// <summary>
        /// The current client credentials
        /// </summary>
        public ClientCredentials ClientCredentials { get; private set; }

        /// <summary>
        /// The current authentication token
        /// </summary>
        public AccessToken AccessToken { get; private set; } = new AccessToken();

        /// <summary>
        /// Sends a GET request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        public async Task<ClientResponse> GetAsync(string url, Dictionary<string, string> urlParameters = null, string correlationId = null)
        {
            return await Task.FromResult(Get<object>(url, urlParameters, correlationId));
        }

        /// <summary>
        /// Sends a GET request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        public async Task<ClientResponse<TResult>> GetAsync<TResult>(string url, Dictionary<string, string> urlParameters = null, string correlationId = null)
        {
            return await Task.FromResult(SendRequest<TResult>(new ClientRequest(url, Method.GET, parameters: urlParameters), correlationId));
        }

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public async Task<ClientResponse> PostAsync(string url, object formData = null, string correlationId = null)
        {
            return await Task.FromResult(Post<object>(url, formData, correlationId));
        }

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public async Task<ClientResponse<TResult>> PostAsync<TResult>(string url, object body = null, string correlationId = null)
        {
            return await Task.FromResult(SendRequest<TResult>(new ClientRequest(url, Method.POST, body: body), correlationId));
        }

        /// <summary>
        /// Sends a POST request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        /// <param name="file">The file to submit with the request</param>
        public async Task<ClientResponse<TResult>> PostAsync<TResult>(string url, Dictionary<string, string> body = null, IFormFile file = null, string correlationId = null)
        {
            var request = new ClientRequest(url, Method.POST, formBody: body, contentTypeHeader: (file != null ? "multipart/form-data" : null));
            if (file != null)
                request.Files.Add(file);

            return await Task.FromResult(SendRequest<TResult>(request, correlationId));
        }

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public async Task<ClientResponse> PutAsync(string url, object formData = null, string correlationId = null)
        {
            return await Task.FromResult(Put<object>(url, formData, correlationId));
        }

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public async Task<ClientResponse<TResult>> PutAsync<TResult>(string url, object body = null, string correlationId = null)
        {
            return await Task.FromResult(SendRequest<TResult>(new ClientRequest(url, Method.PUT, body: body), correlationId));
        }

        /// <summary>
        /// Sends a PUT request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        /// <param name="file">The file to submit with the request</param>
        public async Task<ClientResponse<TResult>> PutAsync<TResult>(string url, Dictionary<string, string> body = null, IFormFile file = null, string correlationId = null)
        {
            var request = new ClientRequest(url, Method.PUT, formBody: body, contentTypeHeader: (file != null ? "multipart/form-data" : null));
            if (file != null)
                request.Files.Add(file);

            return await Task.FromResult(SendRequest<TResult>(request, correlationId));
        }

        /// <summary>
        /// Sends a GET request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        public ClientResponse Get(string url, Dictionary<string, string> urlParameters = null, string correlationId = null)
        {
            return Get<object>(url, urlParameters, correlationId);
        }

        /// <summary>
        /// Sends a GET request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="urlParameters">The parameters of the request</param>
        public ClientResponse<TResult> Get<TResult>(string url, Dictionary<string, string> urlParameters = null, string correlationId = null)
        {
            return SendRequest<TResult>(new ClientRequest(url, Method.GET, parameters: urlParameters), correlationId);
        }

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public ClientResponse Post(string url, object formData = null, string correlationId = null)
        {
            return Post<object>(url, formData, correlationId);
        }

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public ClientResponse<TResult> Post<TResult>(string url, object body = null, string correlationId = null)
        {
            return SendRequest<TResult>(new ClientRequest(url, Method.POST, body: body), correlationId);
        }

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public ClientResponse Put(string url, object formData = null, string correlationId = null)
        {
            return Put<object>(url, formData, correlationId);
        }

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public ClientResponse<TResult> Put<TResult>(string url, object body = null, string correlationId = null)
        {
            return SendRequest<TResult>(new ClientRequest(url, Method.PUT, body: body), correlationId);
        }

        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public ClientResponse Delete(string url, object formData = null, string correlationId = null)
        {
            return Delete<object>(url, formData, correlationId);
        }

        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public ClientResponse<TResult> Delete<TResult>(string url, object body = null, string correlationId = null)
        {
            return SendRequest<TResult>(new ClientRequest(url, Method.DELETE, body: body), correlationId);
        }

        /// <summary>
        /// Sends a DELETE request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="formData">The data to submit with the request</param>
        public async Task<ClientResponse> DeleteAsync(string url, object formData = null, string correlationId = null)
        {
            return await Task.FromResult(Delete<object>(url, formData, correlationId));
        }

        /// <summary>
        /// Sends a DELETE request (async)
        /// </summary>
        /// <param name="url">The url of the request</param>
        /// <param name="body">The data to submit with the request</param>
        public async Task<ClientResponse<TResult>> DeleteAsync<TResult>(string url, object body = null, string correlationId = null)
        {
            return await Task.FromResult(SendRequest<TResult>(new ClientRequest(url, Method.DELETE, body: body), correlationId));
        }

        /// <summary>
        /// Sends a request
        /// </summary>
        /// <param name="configuration">The configuration of the request</param>
        public ClientResponse<TResult> SendRequest<TResult>(ClientRequest configuration, string correlationId = null)
        {
            var authorizationResponseData = Authorize(correlationId);

            return SendRawRequest<TResult>(configuration, authorizationResponseData, correlationId);
        }

        public Uri ClientBaseUrl { get; set; }

        #region Internal

        private void InitializeCommonData(ICacheService cache = null, JsonSerializer serializer = null, IErrorLogger errorLogger = null, string baseUrl = null, ClientCredentials authCredentials = default, string ocpApimSubscriptionKey = "", string accessToken = "", bool usePollyRetry = false, PollySetting pollySetting = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
            {
                if (!baseUrl.EndsWith("/"))
                {
                    baseUrl += "/";
                }

                ClientBaseUrl = new Uri(baseUrl);
            }

            if (!string.IsNullOrEmpty(authCredentials?.ClientId) && !string.IsNullOrEmpty(authCredentials?.ClientSecret))
            {
                if (!authCredentials.IdpUrl.EndsWith("connect/token"))
                {
                    if (!authCredentials.IdpUrl.EndsWith("/")) authCredentials.IdpUrl += "/";

                    authCredentials.IdpUrl += "connect/token";
                }

                ClientCredentials = authCredentials;
            }

            if (!string.IsNullOrWhiteSpace(ocpApimSubscriptionKey))
            {
                OcpApimSubscriptionKey = ocpApimSubscriptionKey;
            }

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                AccessToken = new AccessToken(accessToken);
            }

            if (pollySetting != null)
            {
                PollySetting = pollySetting;
            }
            else
                PollySetting = new();

            UsePollyRetry = usePollyRetry;

            mCache = cache ?? new InMemoryCache();
            mErrorLogger = errorLogger ?? new TraceErrorLogger();

            if (serializer == null)
            {
                serializer = new JsonSerializer();
            }
        }

        /// <summary>
        /// Sends a raw request
        /// </summary>
        /// <param name="requestInfo">The information of the request</param>
        /// <param name="clientAuthResponse">The auth configuration to use when authenticating the request</param>
        private ClientResponse<TResult> SendRawRequest<TResult>(ClientRequest requestInfo, ClientAuthenticationResponse clientAuthResponse = null, string correlationId = null)
        {
            var request = UsePollyRetry ? HttpClientUtil.GetHttpClientFactory(PollySetting).CreateClient("PollyClient") : HttpClientUtil.GetHttpClientFactory(PollySetting).CreateClient();

            request.BaseAddress = ClientBaseUrl;
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(string.IsNullOrWhiteSpace(requestInfo.ContentTypeHeader) ? "application/x-www-form-urlencoded" : requestInfo.ContentTypeHeader));

            if (!string.IsNullOrWhiteSpace(OcpApimSubscriptionKey))
            {
                request.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
            }

            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                request.DefaultRequestHeaders.Add("X-Correlation-ID", correlationId);
            }

            if (clientAuthResponse != null)
            {
                switch (clientAuthResponse.TokenType.ToLower())
                {
                    case "bearer":
                        request.DefaultRequestHeaders.Add("Authorization", $"Bearer {clientAuthResponse.AccessToken}");
                        break;
                }
            }

            if (requestInfo.AdditionalHeaders != null)
            {
                foreach (var header in requestInfo.AdditionalHeaders)
                {
                    request.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (requestInfo.Parameters != null)
            {
                foreach (var parameter in requestInfo.Parameters)
                {
                    request.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                }
            }

            if (requestInfo.Body != null && requestInfo.Method != Method.POST && requestInfo.Method != Method.PUT && requestInfo.Method != Method.DELETE)
            {
                throw new NotSupportedException("The Form data of the configuration can only be supplied if it is a POST or PUT request");
            }

            if (requestInfo.FormBody != null)
            {
                foreach (var item in requestInfo.FormBody)
                {
                    request.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            HttpResponseMessage response = null;

            if (requestInfo.Body != null)
            {
                var payload = new StringContent(JsonConvert.SerializeObject(requestInfo.Body), Encoding.UTF8, "application/json");

                switch (requestInfo.Method)
                {
                    case Method.POST:
                        response = request.PostAsync(requestInfo.Url, payload).Result;
                        break;
                    case Method.PUT:
                        response = request.PutAsync(requestInfo.Url, payload).Result;
                        break;
                    case Method.PATCH:
                        response = request.PatchAsync(requestInfo.Url, payload).Result;
                        break;
                    case Method.DELETE:
                        var req = new HttpRequestMessage(HttpMethod.Delete, requestInfo.Url);
                        req.Content = payload;
                        response = request.SendAsync(req).Result;
                        break;
                }
            }
            else if (requestInfo.Files != null && requestInfo.Files.Count > 0)
            {
                // if request contains file(s), set timeout to maximum
                request.Timeout = TimeSpan.FromMilliseconds(int.MaxValue);

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    foreach (var file in requestInfo.Files)
                    {
                        byte[] fileBytes;
                        using (var filesStream = file.OpenReadStream())
                        using (var memoryStream = new MemoryStream())
                        {
                            filesStream.CopyTo(memoryStream);
                            fileBytes = memoryStream.ToArray();
                        }
                        multipartFormContent.Add(new ByteArrayContent(fileBytes), name: "file", fileName: file.FileName);
                    }
                    switch (requestInfo.Method)
                    {
                        case Method.POST:
                            response = request.PostAsync(requestInfo.Url, multipartFormContent).Result;
                            break;
                        case Method.PUT:
                            response = request.PutAsync(requestInfo.Url, multipartFormContent).Result;
                            break;
                        case Method.PATCH:
                            response = request.PatchAsync(requestInfo.Url, multipartFormContent).Result;
                            break;
                    }
                }
            }

            if (response == null)
            {
                switch (requestInfo.Method)
                {
                    case Method.DELETE:
                        response = request.DeleteAsync(requestInfo.Url).Result;
                        break;
                    case Method.GET:
                        response = request.GetAsync(requestInfo.Url).Result;
                        break;
                    case Method.POST:
                        response = request.PostAsync(requestInfo.Url, new FormUrlEncodedContent(requestInfo.Parameters)).Result;
                        break;
                }
            }

            TResult responseData = default;
            string errorMessage = null;
            ValidationProblemDetails problemDetails = null;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == 0)
                {
                    errorMessage = "Server sent back no response.";
                }
                else
                {
                    errorMessage = EvaluateError(response, out problemDetails);
                }
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                return EvaluateResponse<TResult>(requestInfo, response).Result;
            }

            return new ClientResponse<TResult>(errorMessage, response, responseData, problemDetails);
        }

        /// <summary>
        /// Evaluates error response
        /// </summary>
        private string EvaluateError(HttpResponseMessage response, out ValidationProblemDetails problemDetails)
        {
            problemDetails = null;

            //Return validation errors if the request was sent with invalid data
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(content);

                if (problemDetails != null && problemDetails.Errors == null)
                {
                    return string.Format("{0}: {1}", (int)response.StatusCode, content);
                }
                else if (problemDetails != null && problemDetails.Errors.Count != 0)
                {
                    var errors = problemDetails.Errors.Select(kv => kv.Key + ": " + string.Join(" ", kv.Value));
                    return problemDetails.Title + "\n" + string.Join("\n", errors);
                }
            }
            catch
            {
                //not a valid problem details object
            }

            //At this point, simply return the status code and its description because we were not able to acertain what error we received
            return string.Format("{0}: {1}", (int)response.StatusCode, response.StatusCode.ToString());
        }

        /// <summary>
        /// Evaluates response received from request
        /// </summary>
        private async Task<ClientResponse<TResult>> EvaluateResponse<TResult>(ClientRequest requestInfo, HttpResponseMessage response)
        {
            TResult responseData = default;
            string errorMessage = null;

            if (string.IsNullOrEmpty(errorMessage))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (IsJson(content))
                {
                    try
                    {
                        responseData = JsonConvert.DeserializeObject<TResult>(content);
                    }
                    catch
                    {
                        errorMessage = "Could not deserialize the response. Are you sure " + typeof(TResult).Name + " is the right type?";
                    }
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(content))
                        {
                            //For some reason, if the response is a simple string value it always returns it in
                            //the following format //"\"string\""
                            //(notice the inner quotes)...The following code removes them
                            if (content.StartsWith("\""))
                                content = content.Substring(1, content.Length - 1);

                            if (content.EndsWith("\""))
                                content = content.Substring(0, content.Length - 1);
                        }

                        responseData = string.IsNullOrEmpty(content) ? default : (TResult)Convert.ChangeType(content, typeof(TResult));

                        if (!requestInfo.ResultCanBeNull && string.IsNullOrEmpty(content))
                            errorMessage = "Received empty response.";
                    }
                    catch
                    {
                        throw new Exception("There seems to have been an error converting response data to the expected type. This is the responsed received: " + response.Content);
                    }
                }
            }

            return new ClientResponse<TResult>(errorMessage, response, responseData);
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <returns></returns>
        private ClientAuthenticationResponse Authorize(string correlationId = null)
        {
            // check existing AccessToken first
            if (!AccessToken.IsExpired)
            {
                mAuthResponse = new ClientAuthenticationResponse
                {
                    AccessToken = AccessToken.Token,
                    Expires = AccessToken.Expires,
                    TokenType = "Bearer"
                };
                return mAuthResponse;
            }

            var hasClientCredentials = ClientCredentials != null;

            // throw exception if accestoken is present but expired and no client credentials is available
            if (!string.IsNullOrWhiteSpace(AccessToken.Token) && AccessToken.IsExpired && !hasClientCredentials)
            {
                throw new UnauthorizedAccessException($"AccessToken is expired and client credentials is not available");
            }

            if (!ClientCredentials.IsValid())
            {
                mAuthResponse = null;
                return null;
            }

            // use existing client credentials token
            if (mAuthResponse != null && !mAuthResponse.IsExpired)
            {
                return mAuthResponse;
            }

            // get new client credentials token
            var parameters = new Dictionary<string, string>()
                            {
                                { "grant_type", "client_credentials" }
                            };

            if (!string.IsNullOrEmpty(ClientCredentials.Scope))
            {
                parameters["scope"] = ClientCredentials.Scope;
            }
            if (!string.IsNullOrEmpty(ClientCredentials.ClientId))
            {
                parameters["client_id"] = ClientCredentials.ClientId;
            }
            if (!string.IsNullOrEmpty(ClientCredentials.ClientSecret))
            {
                parameters["client_secret"] = ClientCredentials.ClientSecret;
            }

            var response = SendRawRequest<ClientAuthenticationResponse>(new ClientRequest(ClientCredentials.IdpUrl, Method.POST, contentTypeHeader: "application/x-www-form-urlencoded",
                                                                        parameters: parameters), null, correlationId);

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                mAuthResponse = response.Data;
                mAuthResponse.Expires = DateTimeOffset.UtcNow.AddSeconds(mAuthResponse.ExpiresIn - 5); //not a good idea to use the exact time
            }
            else
            {
                var additionalInfo = !hasClientCredentials ? "" : $"{Environment.NewLine}{ClientCredentials.IdpUrl}:{ClientCredentials.ClientId}:{ClientCredentials.Scope}";
                throw new UnauthorizedAccessException($"Missing/Wrong Credentials or auth URL endpoint. Error Message: {response.ErrorMessage}{additionalInfo}");
            }

            return mAuthResponse;
        }

        /// <summary>
        /// Determines if the supplied string is a json object
        /// </summary>
        private bool IsJson(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            str = str.Trim();

            return (str.StartsWith("{") && str.EndsWith("}") ||
                    str.StartsWith("[") && str.EndsWith("]"));
        }

        #endregion
    }
}
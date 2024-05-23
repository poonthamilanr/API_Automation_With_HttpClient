using System.Net.Http;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    /// <summary>
    /// Response to the client's request
    /// </summary>
    public class ClientResponse
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The data returned by HttpClient
        /// </summary>
        public HttpResponseMessage RestResponse { get; private set; }

        /// <summary>
        /// The problem details (if any)
        /// </summary>
        public ValidationProblemDetails ProblemDetails { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="restResponse">The data returned by RestSharp</param>
        /// <param name="problemDetails">The problem details (if any)</param>
        public ClientResponse(string errorMessage, HttpResponseMessage restResponse, ValidationProblemDetails problemDetails = null)
        {
            this.ErrorMessage = errorMessage;
            this.RestResponse = restResponse;
            this.ProblemDetails = problemDetails;
        }
    }

    /// <summary>
    /// Response to the client's request
    /// </summary>
    public class ClientResponse<TData> : ClientResponse
    {
        /// <summary>
        /// The data returned by the request
        /// </summary>
        public TData Data { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="restResponse">The data returned by RestSharp</param>
        /// <param name="data">The data returned by the request</param>
        /// <param name="problemDetails">The problem details (if any)</param>
        public ClientResponse(string errorMessage, HttpResponseMessage restResponse, TData data, ValidationProblemDetails problemDetails = null) :
            base(errorMessage, restResponse, problemDetails)
        {
            this.Data = data;
        }
    }
}
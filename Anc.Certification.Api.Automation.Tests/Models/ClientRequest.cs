using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Collections.Generic;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    /// <summary>
    /// Describes a new client request
    /// </summary>
    public struct ClientRequest
    {
        /// <summary>
        /// The URL of the request
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The method of the request
        /// </summary>
        public Method Method { get; private set; }

        /// <summary>
        /// Sets the "Content-Type" header. Default value is "application/json"
        /// </summary>
        public string ContentTypeHeader { get; private set; }

        /// <summary>
        /// Sets the "Accept" header. Default value is "application/json"
        /// </summary>
        public string AcceptHeader { get; private set; }

        /// <summary>
        /// True if the returned result is allowed to be null; false to mark null or empty results as errors
        /// </summary>
        public bool ResultCanBeNull { get; private set; }

        /// <summary>
        /// Headers that can't be set using any other properties in this configuration
        /// </summary>
        public Dictionary<string, string> AdditionalHeaders { get; private set; }

        /// <summary>
        /// The parameters to send with the request
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// The form body to send with the request
        /// </summary>
        public Dictionary<string, string> FormBody { get; private set; }

        /// <summary>
        /// The files to send with the request
        /// </summary>
        public List<IFormFile> Files { get; private set; }

        /// <summary>
        /// Data to submit with post request (only supperted in Post or Put requests)
        /// </summary>
        public object Body { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The URL of the request</param>
        /// <param name="method">The method of the request</param>
        /// <param name="contentTypeHeader">Sets the "Content-Type" header. Default value is "application/json"</param>
        /// <param name="acceptHeader">Sets the "Accept" header. Default value is "application/json"</param>
        /// <param name="resultCanBeNull">True if the returned result is allowed to be null; false to mark null or empty results as errors</param>
        /// <param name="additionalHeaders">Headers that can't be set using any other properties in this configuration</param>
        /// <param name="parameters">The parameters to send with the request</param>
        /// <param name="body">Data to submit with post request (only supperted in Post or Put requests)</param>
        public ClientRequest(string url, Method method, string contentTypeHeader = null, string acceptHeader = null, bool resultCanBeNull = false, Dictionary<string, string> additionalHeaders = null, Dictionary<string, string> parameters = null, object body = null, Dictionary<string, string> formBody = null)
        {
            this.Url = url;
            this.Method = method;

            if (contentTypeHeader == null)
            {
                contentTypeHeader = "application/json";
            }

            this.ContentTypeHeader = contentTypeHeader;

            if (acceptHeader == null)
            {
                acceptHeader = "application/json";
            }

            this.AcceptHeader = acceptHeader;
            this.AdditionalHeaders = additionalHeaders;
            this.ResultCanBeNull = resultCanBeNull;
            this.Parameters = parameters;
            this.Body = body;
            this.Files = new List<IFormFile>();
            this.FormBody = formBody;
        }
    }
}
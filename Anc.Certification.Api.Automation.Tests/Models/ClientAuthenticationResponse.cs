using Newtonsoft.Json;
using System;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    /// <summary>
    /// Response received from connect/token call
    /// </summary>
    public class ClientAuthenticationResponse
    {
        /// <summary>
        /// The access token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The type of token this is
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// When this token expires (in seconds)
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Date when the token expires
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Expires { get; set; }

        // <summary>
        /// Flag indicating token is expired or not
        /// </summary>
        [JsonIgnore]
        public bool IsExpired { get { return Expires <= DateTimeOffset.UtcNow; } }
    }
}
using Anc.Certification.Api.Automation.Tests.Provider;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    /// <summary>
    /// Configuration for authorizing a client request
    /// </summary>
    public class ClientCredentials
    {

        protected UserSettings UserSettings;
        /*public ClientCredentials() { 
        UserSettings = new UserSettings();
            this.IdpUrl = UserSettings.IdpBaseUrl;
            this.ClientId = UserSettings.ClientId;
            this.ClientSecret = UserSettings.ClientSecret;
            this.Scope = UserSettings.Scope;
        }*/
        /// <summary>
        /// The auth endpoint
        /// </summary>
        public string IdpUrl { get; set; }

        /// <summary>
        /// The username to use
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// The password to use
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// The scope to use
        /// </summary>
        public string Scope { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idpUrl">The auth endpoint</param>
        /// <param name="clientId">The username to use</param>
        /// <param name="clientSecret">The password to use</param>
        /// <param name="scope">The scope to use</param>
        public ClientCredentials(UserSettings userSettings)
        {
            UserSettings = new UserSettings();
            this.IdpUrl = userSettings.IdpBaseUrl;
            this.ClientId = userSettings.ClientId;
            this.ClientSecret = userSettings.ClientSecret;
            this.Scope = userSettings.Scope;
            this.Username = userSettings.Username;
            this.Password = userSettings.Password;
        }

        /// <summary>
        /// Validates this object
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(IdpUrl) &&
                        !string.IsNullOrEmpty(ClientId) &&
                        !string.IsNullOrEmpty(ClientSecret);
        }

        /// <summary>
        /// Determines if this object is equal to other objects
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = (ClientCredentials)obj;

            return IdpUrl == other.IdpUrl &&
                    ClientId == other.ClientId &&
                    ClientSecret == other.ClientSecret;
        }

        /// <summary>
        /// Gets Hash Code
        /// </summary>
        public override int GetHashCode()
        {
            //return HashCode.Combine(IdpUrl, ClientId, ClientSecret);
            return new { IdpUrl, ClientId, ClientSecret }.GetHashCode();
        }
    }
}
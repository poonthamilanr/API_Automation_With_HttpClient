using Anc.Certification.Api.Automation.Tests.Models;
using Anc.Certification.Api.Automation.Tests.Provider;

namespace Anc.Certification.Api.Automation.Tests.Base
{
    /// <summary>
    /// Base client
    /// </summary>
    public abstract class BaseClient
    {
        /// <summary>
        /// Determines if the client has been initialized
        /// </summary>
        public bool HasBeenInitialized { get; private set; }

        /// <summary>
        /// A reference to the client
        /// </summary>
        protected IClient Client { get; private set; }

        /// <summary>
        /// Initializes the client. 
        /// </summary>
        /// <param name="baseUrl">The base url</param>
        /// <param name="clientCredentials">The credentials</param>
        /// <param name="usePollyRetry">The flag use Polly retry</param>
        public virtual void Initialize(string baseUrl, ClientCredentials clientCredentials)
        {
            Client = new Client(baseUrl, clientCredentials);
            HasBeenInitialized = true;
        }

        /// <summary>
        /// Initializes the client. 
        /// </summary>
        /// <param name="baseUrl">The base url</param>
        /// <param name="accessToken">The authentication token </param>
        /// <param name="ocpApimSubscriptionKey">The apim subscription key</param>        
        public virtual void Initialize(string baseUrl, string accessToken, ClientCredentials clientCredentials)
        {
            Client = new Client(baseUrl, accessToken, clientCredentials);
            HasBeenInitialized = true;
        }


        /// <summary>
        /// Determines if the client needs to be re-initialized. If it does need to be reinitialized, HasBeenInitialized will be set to false
        /// </summary>
        public void VerifyIfReInitializationIsNeeded(string baseUrl, ClientCredentials clientCredentials)
        {
            if (!HasBeenInitialized) return;

            if (Client.ClientBaseUrl.AbsolutePath != baseUrl || !clientCredentials.Equals(Client.ClientCredentials))
            {
                HasBeenInitialized = false;
            }
        }
    }
}

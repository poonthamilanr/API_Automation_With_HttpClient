using Anc.Certification.Api.Automation.Tests.Provider;

namespace Anc.Certification.Api.Automation.Tests.Base
{
    /// <summary>
    /// Used as a base class for all classes sending API requests
    /// </summary>
    public abstract class BaseClientApi
    {
        /// <summary>
        /// A reference to the client
        /// </summary>
        protected IClient Client { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseClientApi(IClient client)
        {
            this.Client = client;
        }
    }
}

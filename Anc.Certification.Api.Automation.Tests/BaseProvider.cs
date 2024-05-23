#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests
{
    using Anc.Certification.Api.Automation.Tests;
    using TechTalk.SpecFlow;
    using Anc.Certification.Api.Automation.Tests.Provider;
    using Unity;
    using Pmi.Api.Framework;
    [Binding]
    public class BaseProvider
    {
        protected UnityContainer Container;
        protected RestServiceTypeBuilder Builder;
        protected IToken TokenClient;
        protected IApplication ApplicationClient;
        protected ICertification CertificationClient;
        protected UserSettings UserSettings;

        protected BaseProvider()
        {
            UserSettings = new UserSettings();
            Container = new UnityContainer();
            Builder = new RestServiceTypeBuilder();
            TokenClient = Builder.GetDynamicRestClientType<IToken>(UserSettings.IdpBaseUrl);
            ApplicationClient = Builder.GetDynamicRestClientType<IApplication>(UserSettings.AppApiUrl);
            CertificationClient = Builder.GetDynamicRestClientType<ICertification>(UserSettings.CertApiUrl);

            Container.RegisterInstance(TokenClient);
            Container.RegisterInstance(ApplicationClient);
            Container.RegisterInstance(CertificationClient);
        }
    }
}

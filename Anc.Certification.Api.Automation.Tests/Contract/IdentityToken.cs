#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Contract
{
    using Anc.Certification.Api.Automation.Tests;
    using Anc.Certification.Api.Automation.Tests.Provider;
    using Authorization = Anc.Certification.Api.Automation.Tests.Models.Authorization;
    using Pmi.Api.Framework;
    using System;
    using Unity;
    using NUnit.Framework;
    using System.Net;

    public class IdentityToken : BaseProvider
    {
        private readonly IToken tokenProvider;

        public IdentityToken()
        {
            tokenProvider = Container.Resolve<IToken>();
        }

        public string liGetIdpToken()
        {
            var response = tokenProvider.GenerateToken(
                UserSettings.Username,
                UserSettings.Password,
                UserSettings.ClientId,
                UserSettings.ClientSecret
            );

            Console.WriteLine(response.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var authorization = response.Content.Deserialize<Authorization>();

            Assert.IsNotNull(authorization.AccessToken);

            String acces_token = authorization.AccessToken.Replace("\"", "").Replace("{", "").Replace("}", "");

            var Token = "Bearer "+ acces_token+"";

            return Token;
        }
    }
}

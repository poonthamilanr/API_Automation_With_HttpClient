#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Provider
{
    using Pmi.Api.Framework;
    using Pmi.Api.Framework.Attributes;
    using RestSharp;
    public interface IToken
    {
        [RestOperation(Resource = "/connect/token", Verb = Method.POST)]
        RestResponse GenerateToken(
            [RequestParam(Name = "username")] string username,
            [RequestParam(Name = "password")] string password,
            [RequestParam(Name = "client_id")] string clientId,
            [RequestParam(Name = "client_secret")] string clientSecret,
            [RequestParam(Name = "scope")] string scope = "CERTAPI CERTPROFILEAPI",
            [RequestParam(Name = "grant_type")] string grantType = "password",
            [Header(Name = "Content-Type")] string contentType = "application/x-www-form-urlencoded");
    }
}

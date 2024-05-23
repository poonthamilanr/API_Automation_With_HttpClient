#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Provider
{
    using Pmi.Api.Framework.Attributes;
    using RestSharp;
    using Anc.Certification.Api.Automation.Tests.Models;
    using Pmi.Api.Framework;
    using RequestBody = Pmi.Api.Framework.Attributes.RequestBody;
    public interface ICertification
    {
        [RestOperation(Resource = "/certification/api/Exams/5006051", Verb = Method.GET)]
        RestResponse GetExamsByPersonID([Header(Name = "Authorization")] string customerToken);
    }
}

#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Provider
{
    using Pmi.Api.Framework.Attributes;
    using RestSharp;
    using Anc.Certification.Api.Automation.Tests.Models;
    using Pmi.Api.Framework;
    using RequestBody = Pmi.Api.Framework.Attributes.RequestBody;
    public interface IApplication
    {
        [RestOperation(Resource = "/5030250/Experience", Verb = Method.POST)]
        RestResponse AddExperience([Header(Name = "Authorization")] string token,
        [RequestBody] ExperienceInfo experienceInfo);        
    }
}

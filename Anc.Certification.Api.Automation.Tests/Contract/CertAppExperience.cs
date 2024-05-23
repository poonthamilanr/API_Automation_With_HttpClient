#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Contract
{
    using Anc.Certification.Api.Automation.Tests.Hooks;
    using Anc.Certification.Api.Automation.Tests.Models;
    using Anc.Certification.Api.Automation.Tests.Provider;
    using NUnit.Framework;
    using RestSharp;
    using SpecFlow.Internal.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;
    using static Anc.Certification.Api.Automation.Tests.Models.ExamResponce;

    public class CertAppExperience : BaseProvider
    {
        private readonly ScenarioContext scenarioContext;
        private readonly UserSettings userSettings;
        private Rootobject _rootobject;
        private ClientCredentials clientCredentials;

        public CertAppExperience(ScenarioContext scenarioCont, ClientCredentials clientCredentials)
        {
            userSettings = new UserSettings();
            scenarioContext = scenarioCont;
            clientCredentials = new ClientCredentials(userSettings);
        }

       /* public RestResponse AddExperience(string token)
        {
            var experience = GetExperienceDetails();
           // var response = application.AddExperience(token, experience);
           // return response;
        }*/

        public void VerifyExperienceAddedSuccessfully(RestResponse responce)
        {
            Assert.AreEqual(HttpStatusCode.Created, responce.StatusCode);
        }

        private ExperienceInfo GetExperienceDetails()
        {
            return new ExperienceInfo
            {
                WorkExperienceTypeEnum = "project",
                ProjectTitle = "Project title",
                Company = "PMI",
                JobTitle = "QA",
                PrimaryFocusTypeEnum = "aerospace",
                FunctionalAreaTypeEnum = "customerService",
                BudgetRangeEnum = "UpToOneMillion",
                MethodologyEnum = 1,
                TeamSizeEnum = 1,
                StartDate = "2017-01-01",
                EndDate = "2020-09-30",
                Description = "This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words. This is a long description of 100 words."
            };
        }

        public Task<ClientResponse> GetExamsTypeByExamID(TestData endPoint)
        {
            var clientCredentials = new ClientCredentials(userSettings);
            var request = new Client(userSettings.AppApiUrl, clientCredentials, false);
            var response = request.GetAsync(endPoint.appEndPoint);
            _rootobject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(response.ToJson());
            if (response.Status.Equals(HttpStatusCode.OK)) Console.WriteLine(response);
            return  response;
        }

        public void GetExamTypeAndVerify(String access_token)
        {
            //var response = certification.GetExamsByPersonID(access_token);
            //_rootobject = response.Content.Deserialize<Rootobject>();
            //Assert.AreEqual("PMP", scenarioContext.Get<String>(CertificationSteps.ExamType));
        }
    }
}

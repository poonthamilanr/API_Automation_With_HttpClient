#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Steps
{
    using Anc.Certification.Api.Automation.Tests.Contract;
    using Anc.Certification.Api.Automation.Tests.Hooks;
    using Anc.Certification.Api.Automation.Tests.Provider;
    using RestSharp;
    using TechTalk.SpecFlow;

    [Binding]
    public class CertificationSteps
    {


        public static string ExamType = "ExamType";

        private readonly CertAppExperience experience;
        private readonly ScenarioContext scenarioContext;
        private readonly Client certificationTestBase;
        private readonly TestData _testData;
        private readonly ScenarioContext _context;

        RestResponse responce;

        public CertificationSteps(ScenarioContext scenarioCont, CertAppExperience exper, TestData testData)
        {
            scenarioContext = scenarioCont;
           // identityToken = identityTok;
            experience = exper;
            _testData = testData;
        }

        [Given(@"User able to get the exam details")]
        public void GivenUserAbleTogetTheExamDetails()
        {
            experience.GetExamsTypeByExamID(_testData);
        }

        [When(@"Add experience")]
        public void WhenAddExperience()
        {
          //  responce = experience.AddExperience(scenarioContext.Get<String>(TokenKey));
        }

        [Then(@"Verify experience is added")]
        public void ThenVerifyExperienceIsAdded()
        {
            experience.VerifyExperienceAddedSuccessfully(responce);
        }

        [Then(@"Verify exam details")]
        public void ThenVerifyExamDetails()
        {
           // experience.GetExamTypeAndVerify(scenarioContext.Get<String>(TokenKey));
        }


    }
}

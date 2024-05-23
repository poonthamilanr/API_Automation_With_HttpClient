using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Anc.Certification.Api.Automation.Tests.Hooks
{
    [Binding]
    public class TestDataInitialization
    {
        private readonly ScenarioContext _context;
        private readonly TestData _testData;
        private readonly UserSettings _userSettings;

        public TestDataInitialization(ScenarioContext context, TestData testData)
        {
            _context = context;
            _testData = testData;
            _userSettings = new UserSettings();
        }

        [Before]
        public void ReadTestDataBeforeTestRun()
        {
            //Should remove this code when reading it from shared location..
            var currentDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                HasHeaderRecord = true
            };
            /*using (var reader = new StreamReader($@"{currentDirectory}\TestData_{_userSettings.Environment}.csv"))
            using (var csv = new CsvReader(reader, csvConfiguration))
            //Reading Testdatas
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (!csv.GetField("ScenarioName").Equals(_context.ScenarioInfo.Title)) continue;
                    _testData.ScenarioName = csv.GetField("ScenarioName");
                    _testData.CertificationType = csv.GetField("Username");
                    _testData.PersonID = csv.GetField("Password");
                    _testData.ExamID = csv.GetField("Password");
                    _testData.ApplicationID = csv.GetField("Password");
                    _testData.CertificationTypeID = csv.GetField("Password");
                    _testData.ClaimID = csv.GetField("Password");
                    _testData.BatchID = csv.GetField("Password");
                    break;
                }
            }*/
            //Reading Application Api EndPoint
            using (var appApiReader = new StreamReader($@"{currentDirectory}\CertificationApiEndPoints\CertAppApiEndPoints.csv"))
            using (var appApiCsv = new CsvReader(appApiReader, csvConfiguration))
            {
                appApiCsv.Read();
                appApiCsv.ReadHeader();
                while (appApiCsv.Read())
                {
                    if (!appApiCsv.GetField("ScenarioName").Equals(_context.ScenarioInfo.Title)) continue;
                    _testData.ScenarioName = appApiCsv.GetField("ScenarioName");
                    _testData.appEndPoint = appApiCsv.GetField("EndPoints");
                    break;
                }
            }
            //Reading Certification Api Endpoint
            using (var certApiReader = new StreamReader($@"{currentDirectory}\CertificationApiEndPoints\CertApiEndPoints.csv"))
            using (var certApiCsv = new CsvReader(certApiReader, csvConfiguration))
            {
                certApiCsv.Read();
                certApiCsv.ReadHeader();
                while (certApiCsv.Read())
                {
                    if (!certApiCsv.GetField("ScenarioName").Equals(_context.ScenarioInfo.Title)) continue;
                    _testData.ScenarioName = certApiCsv.GetField("API_Descriptions/Scenario");
                    _testData.appEndPoint = certApiCsv.GetField("EndPoints");
                    break;
                }
            }
            //Reading Pdu Api Endpoint
            using (var pduApiReader = new StreamReader($@"{currentDirectory}\CertificationApiEndPoints\CertPduApiEndPoints.csv"))
            using (var pduApiCsv = new CsvReader(pduApiReader, csvConfiguration))
            {
                pduApiCsv.Read();
                pduApiCsv.ReadHeader();
                while (pduApiCsv.Read())
                {
                    if (!pduApiCsv.GetField("ScenarioName").Equals(_context.ScenarioInfo.Title)) continue;
                    _testData.ScenarioName = pduApiCsv.GetField("API_Descriptions/Scenario");
                    _testData.appEndPoint = pduApiCsv.GetField("EndPoints");
                    break;
                }
            }
        }
    }
}

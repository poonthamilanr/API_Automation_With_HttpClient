#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Models
{
    using Newtonsoft.Json;
    using System;
    public class ExperienceInfo
    {
        [JsonProperty("workExperienceTypeEnum")] public String WorkExperienceTypeEnum { get; set; }
        [JsonProperty("projectTitle")] public String ProjectTitle { get; set; }
        [JsonProperty("company")] public String Company { get; set; }
        [JsonProperty("jobTitle")] public String JobTitle { get; set; }
        [JsonProperty("primaryFocusTypeEnum")] public String PrimaryFocusTypeEnum { get; set; }
        [JsonProperty("functionalAreaTypeEnum")] public String FunctionalAreaTypeEnum { get; set; }
        [JsonProperty("budgetRangeEnum")] public String BudgetRangeEnum { get; set; }
        [JsonProperty("methodologyEnum")] public int MethodologyEnum { get; set; }
        [JsonProperty("teamSizeEnum")] public int TeamSizeEnum { get; set; }
        [JsonProperty("startDate")] public String StartDate { get; set; }
        [JsonProperty("endDate")] public String EndDate { get; set; }
        [JsonProperty("description")] public String Description { get; set; }
    }
}

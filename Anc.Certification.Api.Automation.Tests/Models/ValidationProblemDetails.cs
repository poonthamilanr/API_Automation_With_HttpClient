using System.Collections.Generic;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    public class ValidationProblemDetails
    {
        public string Detail { get; set; }
        public string Instance { get; set; }
        public int? Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string[]> Errors { get; }
    }
}

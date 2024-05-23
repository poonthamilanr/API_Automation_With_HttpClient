namespace Anc.Certification.Api.Automation.Tests.Models
{
    public class PollySetting
    {
        public int MaxRetries { get; set; } = 2;
        public int InitialWait { get; set; } = 200;
        public int MaxJitter { get; set; } = 500;
    }
}

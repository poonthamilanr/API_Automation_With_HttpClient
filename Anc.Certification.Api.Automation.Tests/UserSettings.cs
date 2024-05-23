#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests
{
    using Microsoft.Extensions.Configuration;
    using Pmi.Web.Ui.Framework;
    using System.Collections.Generic;
    using System.Linq;
    public class UserSettings
    {
        private readonly IEnumerable<IConfigurationSection> _configSection;
        public UserSettings()
        {
            var configManager = new ConfigManager("appsettings.json");
            _configSection = configManager.Configuration.GetSection("UserConfiguration").GetChildren();
        }
        public string AppApiUrl => GetValue("AppApiUrl");
        public string CertApiUrl => GetValue("CertApiUrl");
        public string PduApiUrl => GetValue("PduApiUrl");
        public string IdpBaseUrl => GetValue("IdpBaseUrl");
        public string ClientId => GetValue("ClientId");
        public string ClientSecret => GetValue("ClientSecret");
        public string Username => GetValue("Username");
        public string Password => GetValue("Password");
        public string Scope => GetValue("Scope");
        private string GetValue(string key)
        {
            return _configSection.FirstOrDefault(k => k.Key.Equals(key))?.Value;
        }
        public string Environment => _configSection.FirstOrDefault(k => k.Key.Equals("Environment"))?.Value;
    
    }
}

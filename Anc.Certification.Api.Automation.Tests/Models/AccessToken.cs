using System;
using System.IdentityModel.Tokens.Jwt;

namespace Anc.Certification.Api.Automation.Tests.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset Expires { get; set; } = DateTimeOffset.UtcNow;
        public bool IsExpired { get { return Expires <= DateTimeOffset.UtcNow; } }

        #region " Constructors "

        public AccessToken() { }

        public AccessToken(string jwt)
        {
            try
            {
                if (!string.IsNullOrEmpty(jwt))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);
                    Expires = token.ValidTo;
                    Token = jwt;
                }
            }
            catch
            {
                //swallow exception
            }
        }
        #endregion
    }
}

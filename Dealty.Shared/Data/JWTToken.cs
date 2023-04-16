using Dealty.Shared.Interfaces;

namespace Dealty.Shared.Data
{
    public class JWTToken : IJWTToken
    {
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public byte[] JWTKey { get; set; }
        public JWTToken(string jwtIssuer, string jwtAudience, byte[] jwtKey)
        {
            JWTIssuer = jwtIssuer;
            JWTAudience = jwtAudience;
            JWTKey = jwtKey;
        }
    }
}
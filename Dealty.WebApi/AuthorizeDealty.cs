using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Dealty.WebApi
{
    public class AuthorizeDealty : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeDealty()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
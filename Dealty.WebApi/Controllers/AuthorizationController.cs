using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Dealty.Shared.Interfaces;
using Dealty.WebApi.Logger;

namespace Dealty.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        IDealtyLogger _dealtyLogger;
        IJWTToken _jwtToken;

        public AuthorizationController(IDealtyLogger dealtyLogger, IJWTToken jwtToken)
        {
            _dealtyLogger = dealtyLogger;
            _jwtToken = jwtToken;
        }

        [HttpPost]
        public async Task<ActionResult> CreateToken(Dealty.Shared.Data.UserAuthorization user)
        {
            if (user.UserName == "admin" && user.Password == "Dealty1234!")
            {
                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new[]
                //    {
                //        new Claim("Id", Guid.NewGuid().ToString()),
                //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                //        new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                //        new Claim(JwtRegisteredClaimNames.Jti,
                //        Guid.NewGuid().ToString())
                //     }),
                //    Expires = DateTime.UtcNow.AddMinutes(120),
                //    Issuer = _jwtToken.JWTIssuer,
                //    Audience = _jwtToken.JWTAudience,
                //    SigningCredentials = new SigningCredentials
                //    (new SymmetricSecurityKey(_jwtToken.JWTKey),
                //    SecurityAlgorithms.HmacSha512Signature)
                //};
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var token = tokenHandler.CreateToken(tokenDescriptor);
                //var jwtToken = tokenHandler.WriteToken(token);
                //var stringToken = tokenHandler.WriteToken(token);
                //return Ok(stringToken);

                var secretKey = new SymmetricSecurityKey(_jwtToken.JWTKey);
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _jwtToken.JWTIssuer,
                    audience: _jwtToken.JWTAudience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
        return Unauthorized();
        }
    }
}
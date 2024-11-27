using FogoCertApi.Configuration;
using FogoCertApi.Repository.Interfaces;
using FogoCertApi.Repository.Models;
using FogoCertApi.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Resources;
using System.Security.Claims;
using System.Text;

namespace FogoCertApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IFogoCertApi _portalUserRepository;
        private readonly JwtOptions _jwtOptions;
        public TokensController(IOptions<JwtOptions> jwtOptions, IFogoCertApi PortalUsersRepository)
        {
            _portalUserRepository = PortalUsersRepository;
            _jwtOptions = jwtOptions.Value;
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(TokenViewModel))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken(Authentication authentication)
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);
            TokenViewModel tokenViewModel = new TokenViewModel();
            tokenViewModel.Error = null;

            var portalUserModel = await _portalUserRepository.PortalUserLogin(authentication.Username, authentication.Password);
            if (portalUserModel == null)
            {
                tokenViewModel.Success = true;
                tokenViewModel.UnAuthorizedRequest = true;
                tokenViewModel.Message = "Invalid Username/Password";
                return Unauthorized(tokenViewModel);
            }
            tokenViewModel.Result = new TokenResult();
            tokenViewModel.Result.token = BuildToken(portalUserModel);

            tokenViewModel.Success = true;
            tokenViewModel.UnAuthorizedRequest = false;
            tokenViewModel.Result.isAdministrator = portalUserModel.isAdministrator;
            tokenViewModel.Result.portalUserId = portalUserModel.id;
            return Ok(tokenViewModel);
        }
        private string BuildToken(PortalUser user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuedAt = DateTime.UtcNow;
            var expiresAt = issuedAt.AddMinutes(_jwtOptions.TokenExpirationInMinutes);

            var token = new JwtSecurityToken(
              _jwtOptions.Issuer,
              _jwtOptions.Issuer,
              claims,
              notBefore: issuedAt,
              expires: expiresAt,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

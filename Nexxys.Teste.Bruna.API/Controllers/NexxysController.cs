using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nexxys.Teste.Bruna.API;
using Nexxys.Teste.Bruna.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Teste.Bruna.Nexxys.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NexxysController : ControllerBase
    {
        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.User),
                new Claim("User", userInfo.User),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 5 minutos
            var expiration = DateTime.UtcNow.AddMinutes(5);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UserToken()
            {
                UserName = userInfo.User,
                IsAuthenticate = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration                  
            };
        }


        [HttpPost("AccessToken")]
        public async Task<ActionResult<UserToken>> AccessToken([FromBody] UserInfo userInfo)
        {
            using (var valid = new Validation()) 
            {
                if (valid.LoginValidate(userInfo))
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "login ou senha inválidos.");
                    return BadRequest(ModelState);
                }
            }
                
        }

       

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CheckPassword")]
        public async Task<ActionResult<ValidPassword>> CheckPassword([FromBody] ParamRequest request)
        {
            var validator = new PasswordRequestValidator();
            var result = validator.Validate(new PasswordRequest { CheckPassword = request.Password });
           
            return new ValidPassword {  IsValid  = result.IsValid };

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("CreatePassword")]
        public async Task<ActionResult<ParamRequest>> CreatePassword()
        {
            try
            {
                Guid id = Guid.NewGuid();
                return  new ParamRequest { Password = string.Concat("@A", id, "N#") };
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Ocorreu um erro durante a geração da senha", ex.Message));
            }
            
        }
    }

   
}

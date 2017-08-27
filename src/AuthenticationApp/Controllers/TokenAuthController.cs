using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthenticationApp.Auth;
using Newtonsoft.Json;
using System.Security.Claims;
using AuthenticationApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AuthPolicy")]
    public class TokenAuthController : Controller
    {
        // GET: api/values
        [HttpGet("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public string GetAuthToken([FromBody]User user)
        {
            var existUser = UserStorage.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (existUser != null)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(existUser, expiresIn);

                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        requestAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokeyType = TokenAuthOption.TokenType,
                        accessToken = token                                            
                    }
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Failed,
                    Message = "Username or password is invalid"
                });
            }
        }

        private string GenerateToken(User user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "TokenAuth"),
                new[] {
                    new Claim("ID", user.ID.ToString()),
                    new Claim("Name", user.Name)
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }

        [HttpGet("GetUser")]
        [Authorize("Bearer")]
        public string GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return JsonConvert.SerializeObject(new RequestResult
            {
                State = RequestState.Success,
                Data = new
                {
                    UserName = claimsIdentity.Name
                }
            });
        }
    }    
} 
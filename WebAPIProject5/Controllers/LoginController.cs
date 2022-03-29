using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebAPIProject5.Models;
using WebAPIProject5.Models.Response;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIProject5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        UserInfoDataAccessLayer userInfoDataAccess = new UserInfoDataAccessLayer();
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Login (string username, string pass)
        {
            User login = new User();
            login.UserName = username;
            login.Password = pass;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if(user.UserName!= null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }

        [HttpGet("Authenticate")]
        public AuthenticateResponse Authenticate(string username, string pass)
        {
            User login = new User();
            login.UserName = username;
            login.Password = pass;
            IActionResult response = Unauthorized();
            

            var user = AuthenticateUser(login);

            UserSend usend = new UserSend();
            if (user.UserName != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                return new AuthenticateResponse(user, tokenStr);
            }
            return new AuthenticateResponse(usend, null);
        }

        private UserSend AuthenticateUser(User login)
        {
            UserSend user = userInfoDataAccess.GetLoginUser(login);
            
            return user;
        }
        private string GenerateJSONWebToken(UserSend userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,userinfo.Rolename)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(129),
                signingCredentials: credentials
                ) ;
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to: " + userName;
        }
        
        
        
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}

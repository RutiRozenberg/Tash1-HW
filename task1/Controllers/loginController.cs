using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using task1.Interface;
using Token.Service;
using User.Models;

namespace controller{

    [ApiController]
    [Route("[controller]")]
    public class loginController : ControllerBase{
        IUserInterface UserService;

        public loginController(IUserInterface UserService){
            this.UserService=UserService;
        }
        [HttpPost]
        public ActionResult<string> Login([FromBody] Users User)
        {
            Users userFromData= UserService?.GetAll().FirstOrDefault(u=> u.Name==User.Name && u.Password==User.Password); 
            if(userFromData ==null){
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("id" , userFromData.Id.ToString()),
            };
            if(userFromData.IsAdmin){
                claims.Add(new Claim("type", "Admin"));
            }
            else{
                claims.Add(new Claim("type", "User"));
            }

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));

        }
    }
}
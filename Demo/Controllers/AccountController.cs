using Demo.Core.DTO;
using Demo.EF.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUsrer> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUsrer> userManager , IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUsrer usrer = new ApplicationUsrer();
                usrer.Email = userDto.Email;
                usrer.UserName = userDto.UserName;
                IdentityResult result = await userManager.CreateAsync(usrer , userDto.Password);
                if (result.Succeeded)
                {
                    return Ok("account add sucess");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Logen(LogenUserDto logenUser)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUsrer user = await userManager.FindByNameAsync(logenUser.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, logenUser.Password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:VaildIssuer"],
                            audience: config["JWT:VaildAudiance"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo
                        });

                    }

                }
                return Unauthorized();

            }
            return Unauthorized();
        }


    }
}

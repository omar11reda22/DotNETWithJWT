using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using practise_JWT.Data;
using practise_JWT.DTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace practise_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthantcationController : ControllerBase
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly IConfiguration configuration;
        public AuthantcationController(UserManager<Applicationuser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        // create 2 action 1 - register [post] 2 - Login [post] and create tocken 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dTO)
        {
            if(ModelState.IsValid)
            {
                Applicationuser user = new()
                {
                    UserName = dTO.UserName,
                    Email = dTO.Email,
                    PasswordHash = dTO.Password
                };
               IdentityResult result =  await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                   
                    return Ok("account added success");
                }
                else
                {
                    return BadRequest("sorry something is wrong please try again ..!");
                }
            }
            else
            {
                return BadRequest();    
            }
            
        }
        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDTO dTO)
        {
            if(ModelState.IsValid)
            {
               
              Applicationuser result = await userManager.FindByNameAsync(dTO.Username);
                if(result is not null)
                {
                    //check password
                   bool found =   await userManager.CheckPasswordAsync(result, dTO.Password);
                    if (found is true)
                    {
                        // create claims 
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, result.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:secret"]));
                        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken jwtSecurity = new(
                            issuer: configuration["JWT:issuer"],
                            audience: configuration["JWT:audianse"],
                            expires:DateTime.Now.AddHours(24),
                            claims:claims,
                            signingCredentials: signingCredentials


                            );
                       

                        // create token 
                        return Ok(new { 
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurity),
                        expiration = jwtSecurity.ValidTo
                        
                        });
                    }
                    else
                    {
                        return NotFound("sorry password is wrong");  
                    }
                }
                else
                {
                    return NotFound("sorry user is notfound please register and try again");
                }


            }else
            {
                return Unauthorized();
            }
        }

        
    }
}

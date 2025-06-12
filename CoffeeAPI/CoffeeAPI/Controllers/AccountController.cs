using Application.Service;
using Data.DTO.Authenticate;
using Data.DTO.User;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIMUSIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUpdateUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                EmployeeID = model.EmployeeID,
                CreateDate = DateTime.Now,
                LockoutEnabled = false
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "RegistrationSuccessful" });
            }
            else return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResult>> Login([FromBody] LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var resultUser = new User();
            if (model.Email != "")
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                resultUser = user;
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
            }else
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                resultUser = user;
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
            }
            

                var username = await userManager.FindByNameAsync(model.UserName);
            if (resultUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var result = await signInManager.PasswordSignInAsync(resultUser, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            //Authorization
            var roles = await userManager.GetRolesAsync(resultUser);
            //var permissions = await GetPermissionsByUserIdAsync(user.Id.ToString());
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Email, resultUser.Email),
            new Claim(UserClaims.Id, resultUser.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, resultUser.UserName),
            new Claim(ClaimTypes.Name, resultUser.UserName),
            new Claim(UserClaims.Roles, string.Join(";", roles)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();
            resultUser.RefreshToken = refreshToken;
            resultUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            await userManager.UpdateAsync(resultUser);
            return Ok(new AuthenticatedResult()
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
            //if (result.Succeeded)
            //{
            //    await signInManager.SignInAsync(user, isPersistent: model.RememberMe);
            //    return Ok(new { Message = "User logged in successfully." });
            //}
            //if (result.IsLockedOut)
            //{
            //    return Unauthorized("Invalid account is locks.");
            //}
            //else
            //{
            //    return Unauthorized("Invalid email or password.");
            //}
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    user = new User
                    {
                        UserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(user);
                    if (!result.Succeeded)
                        return BadRequest(result.Errors);
                }

                await signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new
                {
                    message = "Đăng nhập thành công",
                    user = user.Email
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Lỗi xử lý đăng nhập", error = ex.Message });
            }
        }

    }
}

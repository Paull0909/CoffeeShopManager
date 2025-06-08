using Data.DTO.Role;
using Data.DTO.User;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIMUSIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                List<Role> roles = await _roleManager.Roles.ToListAsync();
                return Ok(roles);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(roleModel?.RoleName);
                    if (roleExists)
                    {
                        ModelState.AddModelError("", "Role Already Exists");
                        return BadRequest();
                    }
                    else
                    {
                        Role identityRole = new Role
                        {
                            Name = roleModel?.RoleName,
                            Description = roleModel?.Description
                        };
                        IdentityResult result = await _roleManager.CreateAsync(identityRole);
                        if (result.Succeeded)
                        {
                            return Ok();
                        }
                        foreach (IdentityError error in result.Errors)
                        {
                           return BadRequest(error.Description);
                        }
                    }
                }
                return Ok(roleModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    if (role == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        role.Name = model.RoleName;
                        role.Description = model.Description;
                        var result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded)
                        {
                            return Ok();
                        }
                        else return BadRequest(result.Errors);
                    }
                }
                return Ok(model);
            }
            catch
            {
                return BadRequest(ModelState.ErrorCount);
            }
        }

        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> EditUsersInRole(UserRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.roleId);
            if (role == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            IdentityResult? result;
            if ( !(await _userManager.IsInRoleAsync(user, role.Name)))
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
            }
            else if ( await _userManager.IsInRoleAsync(user, role.Name))
            {
                return BadRequest("Nguoi dung da thuoc quyen han nay");
               // result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            return Ok();
        }

        [HttpGet("GetAllUer")]
        public IActionResult ListUsers()
        {
            try
            {
                var users = _userManager.Users;
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> InfoUser(string UserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return NotFound();
                }
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(UserClaims.Roles, string.Join(";", roles)),
                //new Claim(UserClaims.Permissions, JsonSerializer.Serialize(permissions)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var model = new CreateUpdateUser
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    UserName = user.UserName,
                    Claims = claims.Select(c => c.Value).ToList(),
                    Roles = roles
                };
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Edit Info User")]
        public async Task<IActionResult> EditUser(CreateUpdateUser model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch
            {
                return BadRequest();
            }  
        }

        [HttpGet("ListRolesByUserId")]
        public async Task<IActionResult> ListRolesByUserId(Guid userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var listrole = roles.Select(roleName => new ListRole
                {
                    RoleName = roleName
                }).ToList();
                return Ok(listrole);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

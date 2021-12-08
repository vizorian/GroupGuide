using AutoMapper;
using Group_Guide.Data.Dtos.Campaigns;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Auth;
using Group_Guide.Auth.Model;
using static Group_Guide.Auth.TokenManager;

namespace Group_Guide.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<GroupGuideUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<GroupGuideUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.Username);
            if (user != null)
                return BadRequest("User already exists");

            var newUser = new GroupGuideUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.Username
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user.");

            await _userManager.AddToRoleAsync(newUser, GroupGuideUserRoles.User);
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
                return BadRequest("Username or password is incorrect.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("Username or password is incorrect.");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);

            return Ok(accessToken);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var res = await _tokenManager.VerifyToken(tokenRequest);

                if (res == null)
                {
                    return BadRequest();
                }

                return Ok(res);
            }

            return BadRequest();
        }
    }
}

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

namespace Group_Guide.Controllers
{


    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<GroupGuideUser> _userManager;
        
        public AuthController(UserManager<GroupGuideUser> userManager)
        {
            _userManager = userManager;
        }

        // public AuthController(UserManager<>)

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _use
        }
    }
}

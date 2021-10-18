using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.AuthWithJWT;

namespace HotelListing.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountsController(
            UserManager<User> userManager,
            ILogger<AccountsController> logger,
            IMapper mapper, 
            IAuthManager authManager)
        {
            _userManager = userManager;
            // _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (request is null) return BadRequest("Please fill in the registration form.");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.Password,
                    Email = request.Email,
                    UserName = request.Email
                };

                var result = await _userManager.CreateAsync(user, password: request.Password);

                // If created then add roles to this user
                if (result.Succeeded)
                {
                    if (request.Roles is not null)
                    {
                        var resultRole = await _userManager.AddToRolesAsync(user, request.Roles);
                        return resultRole.Succeeded ? NoContent() : BadRequest("User registration attempt failed.");
                    }
                }

                return BadRequest("User registration attempt failed.");
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,
                    $"Something went wrong in the {nameof(Register)} method in the AccountsController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<IActionResult> Login([FromBody] SignInDTO request)
        {
            if (request is null) return BadRequest("Please fill in the field.");

            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                return await _authManager.ValidateUserAsync(request) ?
                    Accepted(new { Token = await _authManager.CreateTokenAsync() }) 
                    :
                    Unauthorized("Email or Password is invalid. Please try again.");
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,$"Something went wrong in the {nameof(Register)} method in the AccountsController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        } 
    }
}
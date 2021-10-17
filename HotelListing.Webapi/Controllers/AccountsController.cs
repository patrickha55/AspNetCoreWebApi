using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListing.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<User> userManager/*, SignInManager<User> signInManager*/, ILogger<AccountsController> logger, IMapper mapper)
        {
            _userManager = userManager;
            // _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
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
                
                user.UserName = request.Email;
                
                var result = await _userManager.CreateAsync(user,password: request.Password);
                    
                return result.Succeeded ? NoContent() : BadRequest("User registration attempt failed.");
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,$"Something went wrong in the {nameof(Register)} method in the AccountsController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        } 
        
        /*[HttpPost]
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
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);                
                    
                return result.Succeeded ? NoContent() : Unauthorized("User registration attempt failed.");
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,$"Something went wrong in the {nameof(Register)} method in the AccountsController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        } */
    }
}
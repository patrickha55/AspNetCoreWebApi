using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.IRepository;

namespace HotelListing.Webapi.Controllers
{
    [Route("api/[controller]")] // Attribute base routing
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;

        public CountriesController(IUnitOfWork unitOfWork, ILogger<CountriesController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();

                if (countries is null) return NotFound();
                
                var result = _mapper.Map<List<CountryDTO>>(countries);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,$"Something went wrong in the {nameof(GetCountries)} method in the CountriesController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(c => c.Id == id, new List<string> {"Hotels"});

                if (country is null) return NotFound();
                
                var result = _mapper.Map<CountryDTO>(country);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e,$"Something went wrong in the {nameof(GetCountry)} method in the CountriesController.");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
    }
}
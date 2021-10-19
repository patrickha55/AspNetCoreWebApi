using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries([FromQuery] RequestParams request)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                _logger.LogError($"Invalid request in {nameof(GetCountries)}");
                return BadRequest($"Invalid Page Size or Page Number. Please try again.");
            }

            var countries = await _unitOfWork.Countries.GetAll(request);

            if (countries is null) return NotFound("There are no countries in the db yet or something went wrong!");

            var result = _mapper.Map<List<CountryDTO>>(countries);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            if (id < 1) return BadRequest($"Invalid country id. Please try again.");

            var country = await _unitOfWork.Countries.Get(c => c.Id == id, new List<string> { "Hotels" });

            if (country is null) return NotFound();

            var result = _mapper.Map<CountryDTO>(country);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDTO>> Post([FromBody] CreateCountryDTO request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(Post)}.");
                return BadRequest(ModelState);
            }

            var country = _mapper.Map<Country>(request);

            await _unitOfWork.Countries.Insert(country);
            await _unitOfWork.Save();

            var countryDTO = _mapper.Map<CountryDTO>(country);

            return CreatedAtAction(nameof(GetCountry), new { id = countryDTO.Id }, countryDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] ManageHotelDTO request)
        {
            if (!ModelState.IsValid && id < 1)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(Put)}.");
                return BadRequest(ModelState);
            }

            var country = await _unitOfWork.Countries.Get(h => h.Id == id, new List<string> { "Hotels" });

            if (country is null)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(Put)}");
                return NotFound("Hotel with the provided id does not exist.");
            }

            var hotel = _mapper.Map(request, country);

            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(Delete)}.");
                return BadRequest($"Invalid ID provided.");
            }

            var country = await _unitOfWork.Countries.Get(h => h.Id == id);

            if (country is null)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(Delete)}");
                return NotFound("Hotel with the provided id does not exist.");
            }

            await _unitOfWork.Countries.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
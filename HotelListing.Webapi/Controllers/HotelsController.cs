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
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelsController> _logger;
        private readonly IMapper _mapper;

        public HotelsController(IUnitOfWork unitOfWork, ILogger<HotelsController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels([FromQuery] RequestParams request)
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll(request);
                if (hotels is null) return NotFound();

                var result = _mapper.Map<List<HotelDTO>>(hotels);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetHotels)} method in the HotelsController.");
                return StatusCode(500, "Internal sever error. Please try again later.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id, new List<string> { "Country" });
                if (hotel is null) return NotFound();

                var result = _mapper.Map<HotelDTO>(hotel);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetHotel)} method in the HotelsController.");
                return StatusCode(500, "Internal sever error. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDTO>> Post([FromBody] ManageHotelDTO request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(Post)}.");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(request);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();

                var hotelDTO = _mapper.Map<HotelDTO>(hotel);

                return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotelDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Post)} method in the HotelsController.");
                return StatusCode(500, "Internal sever error. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id ,[FromBody] ManageHotelDTO request)
        {
            if (!ModelState.IsValid && id < 1)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(Put)}.");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id);

                if (hotel is null)
                {
                    _logger.LogError($"Invalid PUT attempt in {nameof(Put)}");
                    return NotFound("Hotel with the provided id does not exist.");
                }

                _mapper.Map(request, hotel);

                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Put)} method in the HotelsController.");
                return StatusCode(500, "Internal sever error. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if(id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(Delete)}.");
                return BadRequest($"Invalid ID provided.");
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(h => h.Id == id);

                if (hotel is null)
                {
                    _logger.LogError($"Invalid Delete attempt in {nameof(Delete)}");
                    return NotFound("Hotel with the provided id does not exist.");
                }

                await _unitOfWork.Hotels.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Delete)} method in the HotelsController.");
                return StatusCode(500, "Internal sever error. Please try again later.");
            }
        }
    }
}
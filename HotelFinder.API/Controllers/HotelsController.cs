using HotelFinder.API.Auth;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService? _hotelService;
        private IJwtAuthenticationManager jwtAuthenticationManager;

        
        public HotelsController(IHotelService hotelService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _hotelService = hotelService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// Get All Hotels Data
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); // 200 + data
        }

        /// <summary>
        /// Get Hotels By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]/{id}")] //api/hotels/getbyid/2
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel); //200 +data
            }
            return NotFound(); // 404
        }

        [HttpGet]
        [Route("[action]/{name}")] //api/hotels/getbyid/2

        public async Task<IActionResult> GetByName(string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if (hotel != null) {
                return Ok(hotel);
            }
            return NotFound();
        }

        /// <summary>
        /// Create Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel(Hotel hotel)
        {
            if(ModelState.IsValid)
            {
                var createdHotel = await _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new {id = createdHotel.Id}, createdHotel); //201 + data
            }
            return BadRequest(ModelState);  // 404 validation errors
        }

        /// <summary>
        /// update Hotel data
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if(await _hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));
            }
            return NotFound();
        }

        /// <summary>
        /// delete hotel by ıd
        /// </summary>
        /// <param name="id"></param>

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
               await _hotelService.DeleteHotel(id);
                return Ok(); // 200
            }
            return NotFound();
            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jwtAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}


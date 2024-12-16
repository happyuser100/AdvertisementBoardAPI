using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/advertisementBoard")]
    [ApiController]
    public class AdvertisementBoardController : ControllerBase
    {
        private IAdvertisementBoardRepository _advertisementBoardRepository;

        public AdvertisementBoardController(IAdvertisementBoardRepository advertisementBoardRepository)
        {
            _advertisementBoardRepository = advertisementBoardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ads = await _advertisementBoardRepository.GetAllAsync();
                return Ok(JsonConvert.SerializeObject(ads));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"GetAll:Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("getByPlace/{place}")]
        public async Task<IActionResult> GetByPlace(string place)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ads = await _advertisementBoardRepository.GetByPlaceAsync(place);
                return Ok(JsonConvert.SerializeObject(ads));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"getByPlace:Internal server error: {ex}");
            }
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AdvertisementItem advertisementItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _advertisementBoardRepository.CreateAsync(advertisementItem);
                return Ok(JsonConvert.SerializeObject(advertisementItem));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Create:Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ad = await _advertisementBoardRepository.DeleteAsync(id);

                if (ad == null)
                {
                    return NotFound("Advertisment does not exist");
                }

                return Ok(ad);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Delete:Internal server error: {ex}");
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] AdvertisementItem advertisementItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ad = await _advertisementBoardRepository.UpdateAsync(id, advertisementItem);

                if (ad == null)
                {
                    return NotFound("Advertisment does not exist");
                }

                return Ok(ad);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Update:Internal server error: {ex}");
            }
        }
    }
}

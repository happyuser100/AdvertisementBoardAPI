using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ads = await _advertisementBoardRepository.GetAllAsync();
            return Ok(ads);
        }
    }
}

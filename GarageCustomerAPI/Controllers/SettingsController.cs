using BAL.Services.IService;
using DAL.GlobalAndCommon;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly ISettingsService _service;

        public SettingsController(ISettingsService service, ILogger<SettingsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllSettings")]
        public async Task<IActionResult> GetAllSettings()
        {
            _logger.LogInformation("Getting Settings Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.GetSettings(LocationID: 0);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("GetSettings/{LocationID}")]
        public async Task<IActionResult> GetSettingsByLocationID(int LocationID)
        {
            _logger.LogInformation("Getting Settings Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.GetSettings(LocationID);
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpGet("GetCarMake")]
        public async Task<IActionResult> GetCarMake()
        {
            _logger.LogInformation("Getting CarMake Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.GetCarMake();
            if (result == null) return BadRequest();
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedback(Feedback model)
        {
            _logger.LogInformation("Getting CarMake Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.AddFeedback(model);
            return Ok(new { message = Message.Success, data = result });
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(Review model)
        {
            _logger.LogInformation("Getting CarMake Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.AddReview(model);
            return Ok(new { message = Message.Success, data = result });
        }
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(Review model)
        {
            _logger.LogInformation("Getting CarMake Data..");
            if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");
            var result = await _service.UpdateReview(model);
            return Ok(new { message = Message.Success, data = result });
        }
    }
}

using H2.Application.DTOs;
using H2.Application.Interfaces;
using H2.Application.Usecases;
using H2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThermalImageController : ControllerBase
    {
        private readonly UploadImageHandler _uploadImageHandler;
        private readonly IThermalImageRepository _thermalImageRepository;
        public ThermalImageController(UploadImageHandler uploadImageHandler, IThermalImageRepository thermalImageRepository)
        {
            _uploadImageHandler = uploadImageHandler;
            _thermalImageRepository = thermalImageRepository;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile formFile, [FromForm] string deviceId, [FromForm] string imageType)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var dto = new UploadImageDTO
            {
                Stream = formFile.OpenReadStream(),
                FileName = formFile.FileName,
                ContentType = formFile.ContentType ?? "image/jpeg",
                DeviceId = deviceId,
                ImageType = imageType
            };

            var imageUrl = await _uploadImageHandler.HandleAsync(dto, CancellationToken.None);
            return Ok(new { imageUrl });
        }
        [HttpGet("anomaly-latest")]
        public async Task<ActionResult<IEnumerable<ThermalImage>>> GetLatestAnomalies(string deviceId, CancellationToken cancellationToken)
        {
            var results = await _thermalImageRepository.GetLatestAnomalyAsync(deviceId, cancellationToken);
            return Ok(results);
        }
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<ThermalImage>>> GetLatest(string deviceId, CancellationToken cancellationToken)
        {
            var results = await _thermalImageRepository.GetLatestAsync(deviceId, cancellationToken);
            return Ok(results);
        }
    }
}

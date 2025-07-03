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
    public class SensorController : ControllerBase
    {
        private readonly ISensorDataRepository _sensorDataRepository;
        private readonly UploadSensorDataHandler _uploadSensorDataHandler;
        public SensorController(ISensorDataRepository sensorDataRepository, UploadSensorDataHandler uploadSensorDataHandler)
        {
            _sensorDataRepository = sensorDataRepository ?? throw new ArgumentNullException(nameof(sensorDataRepository));
            _uploadSensorDataHandler = uploadSensorDataHandler ?? throw new ArgumentNullException(nameof(uploadSensorDataHandler));
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSensorDataAsync(string deviceId,CancellationToken cancellationToken)
        {
            var latestData = await _sensorDataRepository.GetLatestAsync(deviceId,cancellationToken);
            if (latestData == null)
            {
                return NotFound("No sensor data found.");
            }
            return Ok(latestData);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadSensorDataAsync([FromBody] UploadSensorDataDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null || string.IsNullOrEmpty(dto.DeviceId))
            {
                return BadRequest("Invalid sensor data.");
            }
            await _uploadSensorDataHandler.HandleAsync(dto, cancellationToken);
            return Ok(dto);
        }
    }
}

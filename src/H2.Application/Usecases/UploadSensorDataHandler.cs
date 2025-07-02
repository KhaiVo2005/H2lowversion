using H2.Application.DTOs;
using H2.Application.Helpers;
using H2.Application.Interfaces;
using H2.Domain.Entities;
using System.Data;

namespace H2.Application.Usecases
{
    public class UploadSensorDataHandler
    {
        private readonly ISensorDataRepository _sensorDataRepository;
        public UploadSensorDataHandler(ISensorDataRepository sensorDataRepository)
        {
            _sensorDataRepository = sensorDataRepository ?? throw new ArgumentNullException(nameof(sensorDataRepository));
        }
        public async Task HandleAsync(UploadSensorDataDTO dto, CancellationToken cancellationToken)
        {
           var alertLevel = AlertEvaluator.Evaluate(dto.HydrogenPpm);
            var data = new SensorData
            {
                Id = Guid.NewGuid(),
                DeviceId = dto.DeviceId,
                HydrogenPpm = dto.HydrogenPpm,
                AlertLevel = alertLevel,
                Timestamp = DateTime.UtcNow
            };
            await _sensorDataRepository.AddAsync(data, cancellationToken);
        }
    }
}

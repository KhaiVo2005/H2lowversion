using H2.Application.DTOs;
using H2.Domain.Entities;

namespace H2.Application.Interfaces
{
    public interface ISensorDataRepository
    {
        Task AddAsync(SensorData sensorData, CancellationToken cancellationToken);
        Task<SensorData?> GetLatestAsync(string deviceId, CancellationToken cancellationToken);
        Task<IEnumerable<SensorData>> GetRecentAsync(string deviceId, int limit, CancellationToken cancellationToken);
    }
}

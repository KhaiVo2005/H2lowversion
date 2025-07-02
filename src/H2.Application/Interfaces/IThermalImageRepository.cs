using H2.Domain.Entities;

namespace H2.Application.Interfaces
{
    public interface IThermalImageRepository
    {
        Task AddAsync(ThermalImage thermalImage, CancellationToken cancellationToken);
        Task<ThermalImage?> GetLatestAsync(string deviceId, CancellationToken cancellationToken);
        Task<ThermalImage?> GetLatestAnomalyAsync(string deviceId, CancellationToken cancellationToken);
    }
}

using H2.Application.Interfaces;
using H2.Domain.Entities;
using H2.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace H2_Infrastructure.Persistence.Repositories
{
    public class ThermalImageRepository : IThermalImageRepository
    {
        private readonly H2DbContext _context;
        public ThermalImageRepository(H2DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(ThermalImage thermalImage, CancellationToken cancellationToken)
        {
            _context.thermalImages.Add(thermalImage);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ThermalImage?> GetLatestAnomalyAsync(string deviceId, CancellationToken cancellationToken)
        {
            return await _context.thermalImages
                .Where(ti => ti.DeviceId == deviceId && ti.ImageType == ImageType.Anomaly)
                .OrderByDescending(ti => ti.Timestamp)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<ThermalImage?> GetLatestAsync(string deviceId, CancellationToken cancellationToken)
        {
            var query = await _context.thermalImages
                .Where(ti => ti.DeviceId == deviceId)
                .OrderByDescending(ti => ti.Timestamp)
                .FirstOrDefaultAsync(cancellationToken);
            return query;
        }
    }
}

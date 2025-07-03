using H2.Application.DTOs;
using H2.Application.Helpers;
using H2.Application.Interfaces;
using H2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2_Infrastructure.Persistence.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly H2DbContext _context;
        public SensorDataRepository(H2DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(SensorData sensorData, CancellationToken cancellationToken)
        {
            _context.sensorDatas.Add(sensorData);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<SensorData?> GetLatestAsync(string deviceId, CancellationToken cancellationToken)
        {
            var query = await _context.sensorDatas
                .Where(sd => sd.DeviceId == deviceId)
                .OrderByDescending(sd => sd.Timestamp)
                .FirstOrDefaultAsync(cancellationToken);
            return query;
        }

        public async Task<IEnumerable<SensorData>> GetRecentAsync(string deviceId, int limit, CancellationToken cancellationToken)
        {
            var query = await _context.sensorDatas
                .Where(sd => sd.DeviceId == deviceId)
                .OrderByDescending(sd => sd.Timestamp)
                .Take(limit)
                .ToListAsync(cancellationToken);
            return query ?? Enumerable.Empty<SensorData>();
        }
    }
}

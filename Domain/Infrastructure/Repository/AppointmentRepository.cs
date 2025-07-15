using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetByChildIdAsync(Guid childId)
    {
        return await _dbSet.Where(a => a.ChildId == childId).ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId)
    {
        return await _dbSet.Where(a => a.DoctorId == doctorId).ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetUpcomingByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(a => a.Child)
            .Where(a => a.Child.MotherId == userId && a.AppointmentDate > DateTime.UtcNow && a.Status == AppointmentStatus.Scheduled)
            .ToListAsync();
    }
} 
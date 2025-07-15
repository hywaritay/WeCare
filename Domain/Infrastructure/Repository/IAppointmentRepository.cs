using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByChildIdAsync(Guid childId);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetUpcomingByUserIdAsync(Guid userId);
} 
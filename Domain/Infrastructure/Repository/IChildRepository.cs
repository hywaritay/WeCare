using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface IChildRepository : IGenericRepository<Child>
{
    Task<IEnumerable<Child>> GetByMotherIdAsync(Guid motherId);
} 
using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(WeCareDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<User?> GetByIdWithChildrenAsync(Guid id)
    {
        return await _dbSet.Include(u => u.Children).FirstOrDefaultAsync(u => u.Id == id);
    }
} 
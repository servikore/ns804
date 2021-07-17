using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EFInMemory
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MemoryDbContext context) : base(context)
        {
        }
        public Task<User> GetByEmail(string email)
        {
            return this.entities.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
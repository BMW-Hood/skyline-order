using Models;
using System.Linq;

namespace Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByPhoneAndPassword(string phone, string password);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MySqlDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
        public User FindByPhoneAndPassword(string phone, string password)
        {
            return DbContext.Users.SingleOrDefault(x => x.Phone == phone && x.Password == password);
        }
    }
}
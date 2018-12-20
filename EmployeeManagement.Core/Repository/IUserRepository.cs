using SmartCity.Core.Entities;

namespace SmartCity.Infrastructure.Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        Users Get(string username);
    }
}
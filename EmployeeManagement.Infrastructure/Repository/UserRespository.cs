using SmartCity.Core.Entities;
using SmartCity.Infrastructure.Repository;
using System.Linq;

namespace SmartCity.Infrastructure
{
    public class UserRespository : Repository<Users>, IUserRepository
    {
        public UserRespository(SmartCityContext context) : base(context)
        {
        }

        public Users Get(string username)
        {
            var query = GetAll().FirstOrDefault(b => b.BlogId == blogId)
            return query;
        }
    }

namespace SmartCity.Infrastructure.Repository
{
    public class UserRespository : Repository<Users>, IUserRepository
    {

        public UserRespository(SmartCityContext context) : base(context)
        {
        }

        public Blog Get(int blogId)
        {
            var query = GetAll().FirstOrDefault(b => b.BlogId == blogId);
            return query;
        }
    }
}

using WebApplication3.Model;

namespace WebApplication3.Services
{
    public class UserService
    {
        private readonly AuthDbContext _AuthDBContext ;

        public UserService(AuthDbContext authDBContext)
        {
            _AuthDBContext = authDBContext;
        }

        public CustomUser getByEmail(string Email)
        {
            return _AuthDBContext.Users.FirstOrDefault(m => m.Email.Equals(Email));
        }

        public String getUserImage(string Email)
        {
            return _AuthDBContext.Users.FirstOrDefault(m => m.Email.Equals(Email)).Photo;
        }

    }

}

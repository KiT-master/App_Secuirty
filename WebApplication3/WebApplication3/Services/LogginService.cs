using WebApplication3.Model;

namespace WebApplication3.Services
{
	public class LogginService
	{
        private readonly AuthDbContext _AuthDBContext;

        public LogginService(AuthDbContext authDBContext)
        {
            _AuthDBContext = authDBContext;
        }

        public void Log(string Email,string Action,string Location)
        {
            var log = new Logging();
            log.Email = Email;
            log.Action = Action;    
            log.Location = Location;
            _AuthDBContext.logging.Add(log);
            _AuthDBContext.SaveChanges();
        }

        public bool checklogOut(string Email)
        {


            return true;
        }
    }
}

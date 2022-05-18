using AspNetCoreWebApiProjManager.Database;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using System.Linq;

namespace AspNetCoreWebApiProjManager.Repository.Classes
{
    public class UserPassRepository : IUserPassRepository
    {
        private readonly DbProjManagerContext db = new();

        public bool Exists(string email)
        {
            return db.TblUserpasses.Any(x => x.Email == email);
        }

        public bool Exists(string email, string password)
        {
            return db.TblUserpasses.Any(x => x.Email == email && x.Password == password);
        }
    }
}

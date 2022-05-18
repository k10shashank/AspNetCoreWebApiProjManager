using AspNetCoreWebApiProjManager.Database;
using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApiProjManager.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly DbProjManagerContext db = new();

        public void Add(UserModel user)
        {
            db.TblUsers.Add(user.GetDbModel());
            db.SaveChanges();
        }

        public void Delete(int userId)
        {
            db.TblTasks.RemoveRange(db.TblTasks.Where(x => x.IdUser == userId));
            db.TblUsers.Remove(db.TblUsers.FirstOrDefault(x => x.IdUser == userId));
            db.SaveChanges();
        }

        public bool Exists(int userId)
        {
            return db.TblUsers.Any(x => x.IdUser == userId);
        }

        public bool Exists(string email)
        {
            return db.TblUsers.Any(x => x.Email == email);
        }

        public IEnumerable<UserModel> Get()
        {
            IEnumerable<TblUser> data = db.TblUsers;
            return from item in data select new UserModel(item);
        }

        public UserModel Get(int userId)
        {
            return new UserModel(db.TblUsers.FirstOrDefault(x => x.IdUser == userId));
        }

        public UserModel Get(string email)
        {
            return new UserModel(db.TblUsers.FirstOrDefault(x => x.Email == email));
        }

        public void Update(UserModel user)
        {
            db.Entry(db.TblUsers.FirstOrDefault(x => x.IdUser == user.ID_USER)).CurrentValues.SetValues(user.GetDbModel());
            db.SaveChanges();
        }
    }
}

using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Add(UserModel user);
        void Delete(int userId);
        bool Exists(int userId);
        bool Exists(string email);
        IEnumerable<UserModel> Get();
        UserModel Get(int userId);
        UserModel Get(string email);
        void Update(UserModel user);
    }
}

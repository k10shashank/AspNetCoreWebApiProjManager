using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Services.Interfaces
{
    public interface IUserService
    {
        void Add(UserModel user);
        void Delete(int userId);
        IEnumerable<UserModel> Get();
        UserModel Get(int userId);
        UserModel Get(string email);
        void Login(string email, string password);
        void Update(UserModel user);
    }
}

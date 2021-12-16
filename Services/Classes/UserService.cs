using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using AspNetCoreWebApiProjManager.Shared;
using System.Collections.Generic;
using System.Net;

namespace AspNetCoreWebApiProjManager.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserPassRepository _userPassRepo;
        public UserService(IUserRepository userRepo, IUserPassRepository userPassRepo)
        {
            _userRepo = userRepo;
            _userPassRepo = userPassRepo;
        }

        public void Add(UserModel user)
        {
            if (_userRepo.Exists(user.ID_USER))
                throw new AppException("User ID already present.", HttpStatusCode.Conflict);
            _userRepo.Add(user);
        }

        public void Delete(int userId)
        {
            CheckUser(userId);
            _userRepo.Delete(userId);
        }

        public IEnumerable<UserModel> Get()
        {
            return _userRepo.Get();
        }

        public UserModel Get(int userId)
        {
            CheckUser(userId);
            return _userRepo.Get(userId);
        }

        public UserModel Get(string email)
        {
            CheckUser(email);
            return _userRepo.Get(email);
        }

        public void Login(string email, string password)
        {
            if (!_userPassRepo.Exists(email))
                throw new AppException("User not present.", HttpStatusCode.NotFound);
            if (!_userPassRepo.Exists(email, password))
                throw new AppException("Password Incorrect.", HttpStatusCode.Unauthorized);
        }

        public void Update(UserModel user)
        {
            CheckUser(user.ID_USER);
            _userRepo.Update(user);
        }

        private void CheckUser(int userId)
        {
            if (!_userRepo.Exists(userId))
                throw new AppException("User not present.", HttpStatusCode.NotFound);
        }

        private void CheckUser(string email)
        {
            if (!_userRepo.Exists(email))
                throw new AppException("User not present.", HttpStatusCode.NotFound);
        }
    }
}

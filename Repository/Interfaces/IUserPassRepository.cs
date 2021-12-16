namespace AspNetCoreWebApiProjManager.Repository.Interfaces
{
    public interface IUserPassRepository
    {
        bool Exists(string email);
        bool Exists(string email, string password);
    }
}

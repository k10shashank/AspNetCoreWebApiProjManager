using AspNetCoreWebApiProjManager.Attributes;
using AspNetCoreWebApiProjManager.Database;

namespace AspNetCoreWebApiProjManager.Entities
{
    public class UserPassModel
    {
        public UserPassModel() { }
        public UserPassModel(TblUserpass userpass)
        {
            EMAIL = userpass.Email;
            PASSWORD = userpass.Password;
        }
        public TblUserpass GetDbModel()
        {
            return new TblUserpass()
            {
                Email = EMAIL,
                Password = PASSWORD
            };
        }
        
        [NotNullCheck]
        public string EMAIL { get; set; }
        
        [NotNullCheck]
        public string PASSWORD { get; set; }
    }
}

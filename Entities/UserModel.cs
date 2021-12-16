using AspNetCoreWebApiProjManager.Attributes;
using AspNetCoreWebApiProjManager.Database;

namespace AspNetCoreWebApiProjManager.Entities
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(TblUser user)
        {
            ID_USER = user.IdUser;
            EMAIL = user.Email;
            FIRST_NAME = user.FirstName;
            LAST_NAME = user.LastName;
        }

        public TblUser GetDbModel()
        {
            return new TblUser()
            {
                IdUser = ID_USER,
                Email = EMAIL,
                FirstName = FIRST_NAME,
                LastName = LAST_NAME
            };
        }

        [NotNullCheck]
        public int ID_USER { get; set; }
        
        [NotNullCheck]
        public string EMAIL { get; set; }
        
        [NotNullCheck]
        public string FIRST_NAME { get; set; }
        
        [NotNullCheck]
        public string LAST_NAME { get; set; }
    }
}

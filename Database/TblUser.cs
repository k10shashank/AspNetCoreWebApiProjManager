using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreWebApiProjManager.Database
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblTasks = new HashSet<TblTask>();
        }

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<TblTask> TblTasks { get; set; }
    }
}

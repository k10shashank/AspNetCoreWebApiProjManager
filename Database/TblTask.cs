using System;

#nullable disable

namespace AspNetCoreWebApiProjManager.Database
{
    public partial class TblTask
    {
        public int IdTask { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }

        public virtual TblProject IdProjectNavigation { get; set; }
        public virtual TblUser IdUserNavigation { get; set; }
    }
}

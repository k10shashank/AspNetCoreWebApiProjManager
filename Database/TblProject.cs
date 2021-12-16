using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreWebApiProjManager.Database
{
    public partial class TblProject
    {
        public TblProject()
        {
            TblTasks = new HashSet<TblTask>();
        }

        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<TblTask> TblTasks { get; set; }
    }
}

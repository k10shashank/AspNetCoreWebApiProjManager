using AspNetCoreWebApiProjManager.Attributes;
using AspNetCoreWebApiProjManager.Database;
using System;

namespace AspNetCoreWebApiProjManager.Entities
{
    public class TaskModel
    {
        public TaskModel() { }
        public TaskModel(TblTask task)
        {
            ID_TASK = task.IdTask;
            DETAILS = task.Details;
            CREATED_ON = task.CreatedOn;
            STATUS = task.Status;
            ID_PROJECT = task.IdProject;
            ID_USER = task.IdUser;
        }

        public TblTask GetDbModel()
        {
            return new TblTask()
            {
                IdTask = ID_TASK,
                Details = DETAILS,
                CreatedOn = CREATED_ON,
                Status = STATUS,
                IdProject = PROJECT.ID_PROJECT,
                IdUser = USER.ID_USER
            };
        }

        [NotNullCheck]
        public int ID_TASK { get; set; }
        
        [NotNullCheck]
        public string DETAILS { get; set; }
        
        [NotNullCheck]
        public DateTime CREATED_ON { get; set; }

        [NotNullCheck]
        [ValidValueCheck("New", "InProgress", "QA", "Completed")]
        public string STATUS { get; set; }

        [NotNullCheck]
        public int ID_PROJECT { get; set; }

        [NotNullCheck]
        public int ID_USER { get; set; }

        public ProjectModel PROJECT { get; set; }
        
        public UserModel USER { get; set; }
    }
}

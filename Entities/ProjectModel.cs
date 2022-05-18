using AspNetCoreWebApiProjManager.Attributes;
using AspNetCoreWebApiProjManager.Database;
using System;

namespace AspNetCoreWebApiProjManager.Entities
{
    public class ProjectModel
    {
        public ProjectModel() { }
        public ProjectModel(TblProject project)
        {
            ID_PROJECT = project.IdProject;
            NAME = project.Name;
            DETAILS = project.Details;
            CREATED_ON = project.CreatedOn;
        }

        public TblProject GetDbModel()
        {
            return new TblProject()
            {
                IdProject = ID_PROJECT,
                Name = NAME,
                Details = DETAILS,
                CreatedOn = CREATED_ON
            };
        }

        [NotNullCheck]
        public int ID_PROJECT { get; set; }

        [NotNullCheck]
        public string NAME { get; set; }

        [NotNullCheck]
        public string DETAILS { get; set; }

        [NotNullCheck]
        public DateTime CREATED_ON { get; set; }
    }
}

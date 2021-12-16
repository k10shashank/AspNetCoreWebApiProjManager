using AspNetCoreWebApiProjManager.Database;
using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApiProjManager.Repository.Classes
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbProjManagerContext db = new DbProjManagerContext();

        public void Add(ProjectModel project)
        {
            db.TblProjects.Add(project.GetDbModel());
            db.SaveChanges();
        }

        public void Delete(int projectId)
        {
            db.TblTasks.RemoveRange(db.TblTasks.Where(x => x.IdProject == projectId));
            db.TblProjects.Remove(db.TblProjects.FirstOrDefault(x => x.IdProject == projectId));
            db.SaveChanges();
        }

        public bool Exists(int projectId)
        {
            return db.TblProjects.Any(x => x.IdProject == projectId);
        }

        public IEnumerable<ProjectModel> Get()
        {
            IEnumerable<TblProject> data = db.TblProjects;
            return from item in data select new ProjectModel(item);
        }

        public ProjectModel Get(int projectId)
        {
            return new ProjectModel(db.TblProjects.FirstOrDefault(x => x.IdProject == projectId));
        }

        public void Update(ProjectModel project)
        {
            db.Entry(db.TblProjects.FirstOrDefault(x => x.IdProject == project.ID_PROJECT)).CurrentValues.SetValues(project.GetDbModel());
            db.SaveChanges();
        }
    }
}

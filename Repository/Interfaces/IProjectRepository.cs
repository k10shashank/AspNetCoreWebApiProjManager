using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Repository.Interfaces
{
    public interface IProjectRepository
    {
        void Add(ProjectModel project);
        void Delete(int projectId);
        bool Exists(int projectId);
        IEnumerable<ProjectModel> Get();
        ProjectModel Get(int projectId);
        void Update(ProjectModel project);
    }
}

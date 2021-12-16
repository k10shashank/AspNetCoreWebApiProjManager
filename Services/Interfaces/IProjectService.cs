using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Services.Interfaces
{
    public interface IProjectService
    {
        void Add(ProjectModel project);
        void Delete(int projectId);
        IEnumerable<ProjectModel> Get();
        ProjectModel Get(int projectId);
        void Update(ProjectModel project);
    }
}

using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using AspNetCoreWebApiProjManager.Shared;
using System.Collections.Generic;
using System.Net;

namespace AspNetCoreWebApiProjManager.Services.Classes
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public void Add(ProjectModel project)
        {
            if (_projectRepo.Exists(project.ID_PROJECT))
                throw new AppException("Project ID already present.", HttpStatusCode.Conflict);
            _projectRepo.Add(project);
        }

        public void Delete(int projectId)
        {
            CheckProject(projectId);
            _projectRepo.Delete(projectId);
        }

        public IEnumerable<ProjectModel> Get()
        {
            return _projectRepo.Get();
        }

        public ProjectModel Get(int projectId)
        {
            CheckProject(projectId);
            return _projectRepo.Get(projectId);
        }

        public void Update(ProjectModel project)
        {
            CheckProject(project.ID_PROJECT);
            _projectRepo.Update(project);
        }

        private void CheckProject(int projectId)
        {
            if (!_projectRepo.Exists(projectId))
                throw new AppException("Project not present.", HttpStatusCode.NotFound);
        }
    }
}

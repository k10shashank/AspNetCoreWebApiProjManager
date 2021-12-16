using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using AspNetCoreWebApiProjManager.Shared;
using System.Collections.Generic;
using System.Net;

namespace AspNetCoreWebApiProjManager.Services.Classes
{
    public class TaskService : ITaskService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly IUserRepository _userRepo;
        public TaskService(IProjectRepository projectRepo, ITaskRepository taskRepo, IUserRepository userRepo)
        {
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
            _userRepo = userRepo;
        }

        public void Add(TaskModel task)
        {
            if (_taskRepo.Exists(task.ID_TASK))
                throw new AppException("Task ID already present.", HttpStatusCode.Conflict);
            _taskRepo.Add(task);
        }

        public void Delete(int taskId)
        {
            CheckTask(taskId);
            _taskRepo.Delete(taskId);
        }

        public IEnumerable<TaskModel> Get()
        {
            IEnumerable<TaskModel> tasks = _taskRepo.Get();
            foreach (TaskModel task in tasks)
                UpdateTask(task);
            return tasks;
        }

        public TaskModel Get(int taskId)
        {
            CheckTask(taskId);
            return UpdateTask(_taskRepo.Get(taskId));
        }

        public void Update(TaskModel task)
        {
            CheckTask(task.ID_TASK);
            _taskRepo.Update(task);
        }

        private void CheckTask(int taskId)
        {
            if (!_taskRepo.Exists(taskId))
                throw new AppException("Task not present.", HttpStatusCode.NotFound);
        }

        private TaskModel UpdateTask(TaskModel task)
        {
            task.PROJECT = _projectRepo.Get(task.ID_PROJECT);
            task.USER = _userRepo.Get(task.ID_USER);
            return task;
        }
    }
}

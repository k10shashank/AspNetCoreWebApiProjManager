using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Services.Interfaces
{
    public interface ITaskService
    {
        void Add(TaskModel task);
        void Delete(int taskId);
        IEnumerable<TaskModel> Get();
        TaskModel Get(int taskId);
        void Update(TaskModel task);
    }
}

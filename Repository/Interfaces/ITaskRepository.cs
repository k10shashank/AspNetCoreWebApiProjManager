using AspNetCoreWebApiProjManager.Entities;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Repository.Interfaces
{
    public interface ITaskRepository
    {
        void Add(TaskModel task);
        void Delete(int taskId);
        bool Exists(int taskId);
        IEnumerable<TaskModel> Get();
        TaskModel Get(int taskId);
        void Update(TaskModel task);
    }
}

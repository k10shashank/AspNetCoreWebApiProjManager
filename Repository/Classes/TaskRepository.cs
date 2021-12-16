using AspNetCoreWebApiProjManager.Database;
using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApiProjManager.Repository.Classes
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbProjManagerContext db = new DbProjManagerContext();

        public void Add(TaskModel task)
        {
            db.TblTasks.Add(task.GetDbModel());
            db.SaveChanges();
        }

        public void Delete(int taskId)
        {
            db.TblTasks.Remove(db.TblTasks.FirstOrDefault(x => x.IdTask == taskId));
            db.SaveChanges();
        }

        public bool Exists(int taskId)
        {
            return db.TblTasks.Any(x => x.IdTask == taskId);
        }

        public IEnumerable<TaskModel> Get()
        {
            IEnumerable<TblTask> data = db.TblTasks;
            return from item in data select new TaskModel(item);
        }

        public TaskModel Get(int taskId)
        {
            return new TaskModel(db.TblTasks.FirstOrDefault(x => x.IdTask == taskId));
        }

        public void Update(TaskModel task)
        {
            db.Entry(db.TblTasks.FirstOrDefault(x => x.IdTask == task.ID_TASK)).CurrentValues.SetValues(task.GetDbModel());
            db.SaveChanges();
        }
    }
}

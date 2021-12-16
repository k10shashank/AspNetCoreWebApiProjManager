using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Controller
{
    public class TaskController : ApiBaseController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public ActionResult Add([FromBody] TaskModel item)
        {
            _taskService.Add(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskModel>> Get()
        {
            return Ok(_taskService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskModel> Get(int id)
        {
            return Ok(_taskService.Get(id));
        }

        [HttpPut]
        public ActionResult Update([FromBody] TaskModel item)
        {
            _taskService.Update(item);
            return NoContent();
        }
    }
}

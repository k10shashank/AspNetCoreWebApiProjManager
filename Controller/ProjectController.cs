using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Controller
{
    public class ProjectController : ApiBaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProjectModel item)
        {
            _projectService.Add(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectModel>> Get()
        {
            return Ok(_projectService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectModel> Get(int id)
        {
            return Ok(_projectService.Get(id));
        }

        [HttpPut]
        public ActionResult Update([FromBody] ProjectModel item)
        {
            _projectService.Update(item);
            return NoContent();
        }
    }
}

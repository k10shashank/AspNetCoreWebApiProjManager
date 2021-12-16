using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreWebApiProjManager.Controller
{
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Add([FromBody] UserModel item)
        {
            _userService.Add(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return Ok(_userService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id)
        {
            return Ok(_userService.Get(id));
        }

        [HttpPut]
        public ActionResult Update([FromBody] UserModel item)
        {
            _userService.Update(item);
            return NoContent();
        }
    }
}

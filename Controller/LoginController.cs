using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiProjManager.Controller
{
    public class LoginController : ApiBaseController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserPassModel item)
        {
            _userService.Login(item.EMAIL, item.PASSWORD);
            return NoContent();
        }

        [HttpGet]
        public ActionResult Blank()
        {
            return Ok();
        }
    }
}

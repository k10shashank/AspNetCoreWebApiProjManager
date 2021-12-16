using AspNetCoreWebApiProjManager.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiProjManager.Controller
{
    [Route("api/[controller]")]
    [Exception]
    public class ApiBaseController : ControllerBase { }
}

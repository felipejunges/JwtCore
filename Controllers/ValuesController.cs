using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace jwtcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize("Bearer", Roles = "Admin")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", User.Identity.Name };
        }
    }
}
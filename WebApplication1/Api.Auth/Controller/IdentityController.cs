using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Auth.Controller
{
    [Route("identity")]
    [Authorize]
    public class IdentityController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var u = from c in User.Claims select new { c.Type,c.Value};
            return new JsonResult(u);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CatService.Controllers
{
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpPost,Route("api/findcat")]
        public string Test()
        {
            return "this is a cat";
        }
        // GET api/values
        [HttpPost, Route("api/get")]
        public string Get()
        {
            return "this is a cat";
        }
    }
}

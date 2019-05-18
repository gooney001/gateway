using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Test.Model;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IFreeSql fsql;
        public ValuesController(IConfiguration configuration, IFreeSql fsql)
        {
            this.configuration = configuration;
            this.fsql = fsql;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var result = fsql.Ado.Query<User>("select * from Users");
            // var rStr = JsonConvert.SerializeObject(result);
            string c = configuration["ReportSet"];
            return new string[] { "value1", "value2", "", c };
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

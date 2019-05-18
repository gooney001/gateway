using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        [Route("image")]
        public FileResult GetImage(string name)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", name);
            //return PhysicalFile(file, "application/octet-stream");
            return PhysicalFile(file, "image/jpg");
        }
        [HttpGet]
        [Route("test")]
        public string GetTest(string name)
        {
            Thread.Sleep(500);
            return name;
        }
    }
}
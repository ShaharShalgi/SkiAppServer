using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiAppServer.Models;

namespace SkiAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkiAppServerAPIController : ControllerBase
    {
        private SkiDBContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public SkiAppServerAPIController(SkiDBContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiAppServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
        [HttpPost("register")]
        public IActionResult Register([FromBody] DTO.VisitorDTO userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                

                //Create model user class
                Models.Visitor modelsUser = userDto.GetModels();

                context.Entry(modelsUser).State = EntityState.Added;
                //context.Visitors.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.VisitorDTO dtoUser = new DTO.VisitorDTO(modelsUser);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("registerPro")]
        public IActionResult RegisterPro([FromBody] DTO.ProfessionalDTO proDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt



                //Create model user class
                Models.Professional modelsPro = proDto.GetModels();

                context.Entry(modelsPro).State = EntityState.Added;
                //context.Visitors.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.ProfessionalDTO dtoUser = new DTO.ProfessionalDTO(modelsPro);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.VisitorDTO loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching password
                Models.Visitor? modelsUser = context.GetVisitor(loginDto.Pass);

                //Check if user exist for this password match, if not return Access Denied (Error 403) 
                if (modelsUser == null || modelsUser.Pass != loginDto.Pass)
                {
                    return Unauthorized();
                }

                //Login suceed! now mark login in session memory!
                HttpContext.Session.SetString("loggedInUser", modelsUser.Username);

                DTO.VisitorDTO dtoUser = new DTO.VisitorDTO(modelsUser);

                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}



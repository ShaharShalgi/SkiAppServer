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
        [HttpGet("getTips")]
        public IActionResult GetTips()
        {
            try
            {
                List<Models.Tip> listTips = context.GetAllTips();
                return Ok(listTips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getPro")]
        public IActionResult GetPro(int Id)
        {
            try
            {
               Professional Pro = context.GetPro(Id);
                return Ok(Pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sortTips")] 
        public IActionResult SortTips(int diff)
        {
            try
            {
                List<Models.Tip> sortedTips = context.GetTipsByDifficulty(diff);
                return Ok(sortedTips);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("updateUser")]      
        public async Task<IActionResult> UpdateProfile([FromBody] DTO.VisitorDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            // חיפוש המשתמש לפי Id
            var user = await context.Visitors.FindAsync(userDto.UserId);

            if (user == null)
            {
                return NotFound($"User with ID {userDto.UserId} not found");
            }

            // עדכון השדות של המשתמש
            user.Username = userDto.Username;
            user.Pass = userDto.Pass;
            user.Gender = userDto.Gender;
            user.Email = userDto.Email;

            try
            {
                // שמירת השינויים למסד הנתונים
                await context.SaveChangesAsync();
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", error = ex.Message });
            }

        }
        [HttpPost("updatePro")]
        public async Task<IActionResult> UpdateProfessional([FromBody] DTO.ProfessionalDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            // חיפוש המשתמש לפי Id
            var user = await context.Professionals.FindAsync(userDto.UserId);

            if (user == null)
            {
                return NotFound($"User with ID {userDto.UserId} not found");
            }

            // עדכון השדות של המשתמש
            user.Price = userDto.Price;
            user.Txt = userDto.Txt;
            user.Loc = userDto.Loc;
            user.TypeId = userDto.TypeId;

            try
            {
                // שמירת השינויים למסד הנתונים
                await context.SaveChangesAsync();
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", error = ex.Message });
            }

        }

        [HttpPost("UploadPostImage")]
        public async Task<IActionResult> UploadPostImageAsync(IFormFile file, [FromQuery] int posterId)
        {
            //Check if who is logged in
            string? username = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model pro class from DB with matching id. 
            Models.Visitor? poster = context.GetVisitorById(posterId);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (poster == null)
            {
                return Unauthorized("User is not found in the database");
            }

            //Add photo to database (only the record)
            PostPhoto photoRecord = new PostPhoto() { UserId = posterId };
            context.PostPhotos.Add(photoRecord);
            context.SaveChanges();

            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    return BadRequest("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\toilets\\{photoRecord.PhotoId}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                    }

                }

            }

            poster.PostPhotos.Add(photoRecord);
            DTO.VisitorDTO dtoVisitor = new DTO.VisitorDTO(poster, this.webHostEnvironment.WebRootPath);
            return Ok(dtoVisitor);
        }

        //Helper functions

        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }


    }
}



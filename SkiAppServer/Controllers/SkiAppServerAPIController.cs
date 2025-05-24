using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiAppServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json;
using SkiAppServer.DTO;

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
        [HttpPost("UploadReview")]
        public IActionResult UploadReview([FromBody] DTO.ReviewDTO reviewDto)
        {
            try
            {
                //Check if user is logged in
                string? user = HttpContext.Session.GetString("loggedInUser");

                if (user == null || user == "")
                {
                    return Unauthorized();
                }

                //Create model user class
                Models.Review modelsReview = reviewDto.GetModels();

               

                context.Reviews.Add(modelsReview);
                context.SaveChanges();

                //Toilet was added!

                string photosLocalPath = webHostEnvironment.WebRootPath;
                DTO.ReviewDTO dtoReview = new DTO.ReviewDTO(modelsReview, photosLocalPath);
                string json = JsonSerializer.Serialize(dtoReview);
                return Ok(dtoReview);
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
        [HttpGet("getCoaches")]
        public IActionResult GetCoaches()
        {
            try
            {
                List<Models.Professional> listPosts = context.GetAllCoaches();
                return Ok(listPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getResorts")]
        public IActionResult GetResorts()
        {
            try
            {
                List<Models.Professional> listPosts = context.GetAllResorts();
                return Ok(listPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getReviewsByProID")]
        public IActionResult GetReviewsPro(int Id)
        {
            try
            {
                List<Models.Review> listReviews = context.GetReviewsByProID(Id);
                return Ok(listReviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getReviewByID")]
        public IActionResult GetReview(int Id)
        {
            try
            {
                Models.Review review = context.GetReviewById(Id);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<Models.Visitor> visitors = context.GetAllVisitors();
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllPostPhotos")]
        public IActionResult GetAllPostPhotos()
        {
            try
            {
                List<Models.PostPhoto> postPhotos = context.GetAllPostPhotos();
                return Ok(postPhotos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllReviewPhotos")]
        public IActionResult GetAllReviewPhotos()
        {
            try
            {
                List<Models.ReviewPhoto> reviewPhotos = context.GetAllReviewPhotos();
                return Ok(reviewPhotos);
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
        [HttpGet("getUser")]
        public IActionResult GetUser(int Id)
        {
            try
            {
                Visitor User = context.GetVisitorById(Id);
                return Ok(User);
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
        //[HttpGet("sortPricesCoachASC")]
        //public IActionResult SortPricesCoachASC()
        //{
        //    try
        //    {
        //        List<Models.Professional> sortedPrices = context.GetPostByPriceCoachASC();
        //        return Ok(sortedPrices);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("sortPricesCoachDESC")]
        //public IActionResult SortPricesCoachDESC()
        //{
        //    try
        //    {
        //        List<Models.Professional> sortedPrices = context.GetPostByPriceCoachDESC();
        //        return Ok(sortedPrices);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("sortRatingsCoachDESC")]
        //public IActionResult SortRatingsCoachDESC()
        //{
        //    try
        //    {
        //        List<Models.Professional> sortedRatings = context.GetPostByRatingCoachDESC();
        //        return Ok(sortedRatings);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("sortRatingsCoachASC")]
        //public IActionResult SortRatingsCoachASC()
        //{
        //    try
        //    {
        //        List<Models.Professional> sortedRatings = context.GetPostByRatingCoachASC();
        //        return Ok(sortedRatings);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("sortCoachesByPriceAndRating")]
        public IActionResult SortCoachesByPriceAndRating(bool priceAscending, bool ratingAscending)
        {
            try
            {
                List<Models.Professional> coaches = context.GetAllCoaches();

                // Sort by both criteria - first by price, then by rating
                if (priceAscending)
                {
                    if (ratingAscending)
                        coaches = coaches.OrderBy(c => c.Price).ThenBy(c => c.Rating).ToList();
                    else
                        coaches = coaches.OrderBy(c => c.Price).ThenByDescending(c => c.Rating).ToList();
                }
                else
                {
                    if (ratingAscending)
                        coaches = coaches.OrderByDescending(c => c.Price).ThenBy(c => c.Rating).ToList();
                    else
                        coaches = coaches.OrderByDescending(c => c.Price).ThenByDescending(c => c.Rating).ToList();
                }

                return Ok(coaches);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("sortResortsByPriceAndRating")]
        public IActionResult SortResortsByPriceAndRating(bool priceAscending, bool ratingAscending)
        {
            try
            {
                List<Models.Professional> resorts = context.GetAllResorts();

                // Sort by both criteria - first by price, then by rating
                if (priceAscending)
                {
                    if (ratingAscending)
                        resorts = resorts.OrderBy(c => c.Price).ThenBy(c => c.Rating).ToList();
                    else
                        resorts = resorts.OrderBy(c => c.Price).ThenByDescending(c => c.Rating).ToList();
                }
                else
                {
                    if (ratingAscending)
                        resorts = resorts.OrderByDescending(c => c.Price).ThenBy(c => c.Rating).ToList();
                    else
                        resorts = resorts.OrderByDescending(c => c.Price).ThenByDescending(c => c.Rating).ToList();
                }

                return Ok(resorts);
            }
            catch (Exception ex)
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
            user.IsPro = userDto.IsPro;

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
            user.Post = userDto.Post;
            user.RaterNum = userDto.RaterNum;
            user.Rating = userDto.Rating;

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

        [HttpPost("UploadReviewImage")]
        public async Task<IActionResult> UploadReviewImageAsync(IFormFile file, [FromQuery] int reviewId)
        {
            //Check if who is logged in
            string? username = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model pro class from DB with matching id. 
            Models.Review? review = context.GetReviewById(reviewId);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (review == null)
            {
                return Unauthorized("Review is not found in the database");
            }

            //Add photo to database (only the record)
            ReviewPhoto photoRecord = new ReviewPhoto() { ReviewId = reviewId };
            context.ReviewPhotos.Add(photoRecord);
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
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\reviews\\{photoRecord.PhotoId}{extention}";

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


            review.ReviewPhotos.Add(photoRecord);
            DTO.ReviewDTO dtoReview = new DTO.ReviewDTO(review, this.webHostEnvironment.WebRootPath);
            return Ok(dtoReview);
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
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\posts\\{photoRecord.PhotoId}{extention}";

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
        [HttpPost("RemovePostImage")]
        public async Task <IActionResult> RemovePostImage([FromBody] DTO.PostPhotoDTO postDTO)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt



                //Create model user class
                Models.PostPhoto modelsPost = postDTO.GetModels();

                context.Remove(modelsPost).State = EntityState.Deleted;

                //context.Visitors.Add(modelsUser);
                context.SaveChanges();

                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetPostImages")]      
        public IActionResult GetPostImages(int posterId)
        {
            try
            {
                List<Models.PostPhoto> photos = context.GetPostPhotos(posterId);
                List<string> paths = new List<string>();
                foreach(Models.PostPhoto p in photos)
                {
                    paths.Add(GetPostImageVirtualPath(p.PhotoId));
                }

                return Ok(paths);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetReviewImages")]
        public IActionResult GetReviewImages(int reviewId)
        {
            try
            {
                List<Models.ReviewPhoto> photos = context.GetReviewPhotos(reviewId);
                List<string> paths = new List<string>();
                foreach (Models.ReviewPhoto p in photos)
                {
                    paths.Add(GetReviewImageVirtualPath(p.PhotoId));
                }

                return Ok(paths);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GetPostImageVirtualPath(int photoID)
        {
            string virtualPath = $"/posts/{photoID}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\posts\\{photoID}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\posts\\{photoID}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/posts/default.png";
                }
            }

            return virtualPath;
        }
        private string GetReviewImageVirtualPath(int photoID)
        {
            string virtualPath = $"/reviews/{photoID}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\reviews\\{photoID}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\reviews\\{photoID}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/reviews/default.png";
                }
            }

            return virtualPath;
        }

        [HttpGet("GetPostPath")]
        public string GetPostPath(int photoID)
        {
            string virtualPath = $"{photoID}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\posts\\{photoID}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\posts\\{photoID}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/posts/default.png";
                }
            }

            return virtualPath;
        }
        [HttpGet("GetReviewPath")]
        public string GetReviewPath(int photoID)
        {
            string virtualPath = $"{photoID}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\reviews\\{photoID}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\reviews\\{photoID}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/reviews/default.png";
                }
            }

            return virtualPath;
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



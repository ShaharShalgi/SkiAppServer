using SkiAppServer.Models;
namespace SkiAppServer.DTO
{
    public class VisitorDTO
    {
        public string Username { get; set; } = null; 
        public string Pass { get; set; } = null;
        public string? Gender { get; set; } = null;
        public string? Email { get; set; } = null;
        public int UserId { get; set; }
        public bool? IsPro { get; set; } = null;
        public string PhotoURL { get; set; } = "";
        public List<PostPhotoDTO>?  Photos { get; set; } 
        public VisitorDTO() { }
        public VisitorDTO(Models.Visitor modelVisitor) 
        {
            this.Username = modelVisitor.Username;
            this.Pass = modelVisitor.Pass;
            this.Gender = modelVisitor.Gender;
            this.Email = modelVisitor.Email;
            this.UserId = modelVisitor.UserId;
            this.IsPro = modelVisitor.IsPro;
       
        }
        public Models.Visitor GetModels()
        {
            Models.Visitor modelsUser = new Models.Visitor()
            {
                UserId = this.UserId,
                Username = this.Username,               
                Email = this.Email,
                Pass = this.Pass,
                Gender = this.Gender,
                IsPro = this.IsPro
                
            };

            return modelsUser;
        }

        public VisitorDTO(Models.Visitor modelVisitor, string photoBasePath)
        {
            this.UserId = modelVisitor.UserId;
            this.Pass = modelVisitor.Pass;
            this.Gender = modelVisitor.Gender;
            this.Email = modelVisitor.Email;
            this.UserId = modelVisitor.UserId;
            this.IsPro = modelVisitor.IsPro;
           
            this.Photos = new List<PostPhotoDTO>();
            if (modelVisitor.PostPhotos != null)
            {
                foreach (PostPhoto p in modelVisitor.PostPhotos)
                {
                    this.Photos.Add(new PostPhotoDTO()
                    {
                        PhotoId = p.PhotoId,
                        PhotoUrlPath = GetPostPhotoPath(p.PhotoId, photoBasePath)
                    });
                }
            }
        }
        private string GetPostPhotoPath(int photoId, string photoBasePath)
        {
            string virtualPath = $"/posts/{photoId}";
            string path = $"{photoBasePath}\\posts\\{photoId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{photoBasePath}\\posts\\{photoId}.jpg";
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
    }


}


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SkiAppServer.Models;

namespace SkiAppServer.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

     
        public int? RecieverId { get; set; }

       
        public int? SenderId { get; set; }

        public int? Rating { get; set; }

        public string? Title { get; set; }
        public string? Txt { get; set; }

        public string PhotoURL { get; set; } = "";
        public List<ReviewPhotosDTO>? Photos { get; set; }

        public Models.Review GetModels()
        {
            Models.Review modelsReview = new Models.Review()
            {
                ReviewId = this.ReviewId,
                SenderId = this.SenderId,
                RecieverId = this.RecieverId,
                Rating = this.Rating,
                Title = this.Title,
                Txt = this.Txt

            };

            return modelsReview;
        }
        public ReviewDTO() { }
        public ReviewDTO(Models.Review modelReview) 
        {
            this.ReviewId = modelReview.ReviewId;
            this.RecieverId = modelReview.RecieverId;
            this.Title = modelReview.Title;
            this.Txt = modelReview.Txt;
            this.Rating = modelReview.Rating;
            this.SenderId = modelReview.SenderId;

        }
        public ReviewDTO(Models.Review modelReview, string photoBasePath) 
        {
        this.ReviewId = modelReview.ReviewId;
        this.RecieverId = modelReview.RecieverId;
        this.Title = modelReview.Title;
        this.Txt = modelReview.Txt;
        this.Rating = modelReview.Rating;
        this.SenderId = modelReview.SenderId;
            this.Photos = new List<ReviewPhotosDTO>();
            if (modelReview.ReviewPhotos != null)
            {
                foreach (ReviewPhoto p in modelReview.ReviewPhotos)
                {
                    this.Photos.Add(new ReviewPhotosDTO()
                    {
                        PhotoId = p.PhotoId,
                        PhotoUrlPath = GetReviewPhotoPath(p.PhotoId, photoBasePath)
                    });
                }
            }
        }

        private string GetReviewPhotoPath(int photoId, string photoBasePath)
        {
            string virtualPath = $"/reviews/{photoId}";
            string path = $"{photoBasePath}\\reviews\\{photoId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{photoBasePath}\\reviews\\{photoId}.jpg";
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


    }
}

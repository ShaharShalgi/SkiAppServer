namespace SkiAppServer.DTO
{
    public class ReviewPhotosDTO
    {
        public int PhotoId { get; set; }
        public int? ReviewId { get; set; }
        public ReviewPhotosDTO() { }
        public ReviewPhotosDTO(Models.ReviewPhoto modelReview)
        {
            this.PhotoId = modelReview.PhotoId;
            this.ReviewId = modelReview.ReviewId;
            
        }

    }
}

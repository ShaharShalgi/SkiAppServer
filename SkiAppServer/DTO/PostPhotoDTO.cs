namespace SkiAppServer.DTO
{
    public class PostPhotoDTO
    {
        public int PhotoId { get; set; }
        public int? UserID { get; set; }
        public string PhotoUrlPath { get; set; }

        public PostPhotoDTO() { }
        public PostPhotoDTO(Models.PostPhoto modelPost)
        {
            this.PhotoId = modelPost.PhotoId;
            this.UserID = modelPost.UserId;

        }
    }
}

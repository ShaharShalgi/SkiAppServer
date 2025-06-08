namespace SkiAppServer.DTO
{
    public class PostPhotoDTO
    {
        public int PhotoId { get; set; }
        public int? UserID { get; set; }
        public string PhotoUrlPath { get; set; }

        public PostPhotoDTO() { }
        public PostPhotoDTO(Models.ח modelPost)
        {
            this.PhotoId = modelPost.PhotoId;
            this.UserID = modelPost.UserId;

        }
        public Models.ח GetModels()
        {
            Models.ח modelsUser = new Models.ח()
            {
                UserId = this.UserID,
               PhotoId = this.PhotoId,
               

            };

            return modelsUser;
        }
    }
}

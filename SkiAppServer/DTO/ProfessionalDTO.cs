namespace SkiAppServer.DTO
{
    public class ProfessionalDTO
    {
        public int UserId { get; set; }

        public double? Rating { get; set; }


        public int? TypeId { get; set; }


        public string? Loc { get; set; }

        public double? Price { get; set; }
       
        public int? RaterNum { get; set; }
        
        public bool? Post { get; set; }



        public string? Txt { get; set; }
        public ProfessionalDTO() { }

        public ProfessionalDTO(Models.Professional modelProfessional)
        {
            this.UserId = modelProfessional.UserId;
            this.Rating = modelProfessional.Rating;
            this.TypeId = modelProfessional.TypeId;
            this.Loc = modelProfessional.Loc;
            this.Price = modelProfessional.Price;
            this.Txt = modelProfessional.Txt;
            this.RaterNum = modelProfessional.RaterNum;
            this.Post = modelProfessional.Post;
        }
        public Models.Professional GetModels()
        {
            Models.Professional modelsPro = new Models.Professional()
            {
                UserId = this.UserId,
                Rating = this.Rating,
                TypeId = this.TypeId,
                Loc = this.Loc,
                Price = this.Price,
                Txt = this.Txt,
                RaterNum = this.RaterNum,
                Post = this.Post

            };
            return modelsPro;
        }
    }
}

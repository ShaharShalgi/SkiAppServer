namespace SkiAppServer.DTO
{
    public class ProffesionalDTO
    {
        public int UserId { get; set; }

        public double? Rating { get; set; }


        public int? TypeId { get; set; }


        public string? Loc { get; set; }

        public double? Price { get; set; }


        public string? Txt { get; set; }
        public ProffesionalDTO() { }

        public ProffesionalDTO(Models.Proffesional modelProffesional)
        {
            this.UserId = modelProffesional.UserId;
            this.Rating = modelProffesional.Rating;
            this.TypeId = modelProffesional.TypeId;
            this.Loc = modelProffesional.Loc;
            this.Price = modelProffesional.Price;
            this.Txt = modelProffesional.Txt;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SkiAppServer.DTO
{
    public class TipDTO
    {
        public int TipId { get; set; }


        public string? Difficulty { get; set; }


        public string? Topic { get; set; }

   
        public string? Title { get; set; }

      
        public string? Info { get; set; }

      
        public string? VideoLink { get; set; }

        public TipDTO() { }
        public TipDTO(Models.Tip modelTip) 
        {
            this.TipId = modelTip.TipId;
            this.Difficulty = modelTip.Difficulty;
            this.Topic = modelTip.Topic;
            this.Title = modelTip.Title;
            this.Info = modelTip.Info;
            this.VideoLink = modelTip.VideoLink;
        }
    }
}

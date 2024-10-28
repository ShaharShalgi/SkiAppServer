using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkiAppServer.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

     
        public int? RecieverId { get; set; }

       
        public int? SenderId { get; set; }

        public bool? IsPositive { get; set; }

        
        public string? Txt { get; set; }

        public ReviewDTO() { }
        public ReviewDTO(Models.Review modelReview) 
        {
        this.ReviewId = modelReview.ReviewId;
        this.RecieverId = modelReview.RecieverId;
        this.Txt = modelReview.Txt;
        this.IsPositive = modelReview.IsPositive;
        this.SenderId = modelReview.SenderId;
        }


    }
}

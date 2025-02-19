using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

[Table("REVIEWS")]
public partial class Review
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("RecieverID")]
    public int? RecieverId { get; set; }

    [Column("SenderID")]
    public int? SenderId { get; set; }

    public bool? IsPositive { get; set; }

    [StringLength(150)]
    public string? Txt { get; set; }

    [ForeignKey("RecieverId")]
    [InverseProperty("ReviewRecievers")]
    public virtual Visitor? Reciever { get; set; }

    [InverseProperty("Review")]
    public virtual ICollection<ReviewPhoto> ReviewPhotos { get; set; } = new List<ReviewPhoto>();

    [ForeignKey("SenderId")]
    [InverseProperty("ReviewSenders")]
    public virtual Visitor? Sender { get; set; }
}

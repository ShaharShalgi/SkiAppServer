using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class ReviewPhoto
{
    [Key]
    public int PhotoId { get; set; }

    [Column("ReviewID")]
    public int? ReviewId { get; set; }

    [ForeignKey("ReviewId")]
    [InverseProperty("ReviewPhotos")]
    public virtual Review? Review { get; set; }
}

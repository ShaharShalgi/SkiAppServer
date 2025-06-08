using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class ח
{
    [Key]
    public int PhotoId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PostPhotos")]
    public virtual Visitor? User { get; set; }
}

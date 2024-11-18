using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class Professional
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    public double? Rating { get; set; }

    [Column("TypeID")]
    public int? TypeId { get; set; }

    [StringLength(40)]
    public string? Loc { get; set; }

    public double? Price { get; set; }

    [StringLength(300)]
    public string? Txt { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("Professionals")]
    public virtual TypeUser? Type { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Professional")]
    public virtual Visitor User { get; set; } = null!;
}

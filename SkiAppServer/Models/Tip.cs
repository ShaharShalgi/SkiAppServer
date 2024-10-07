using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class Tip
{
    [Key]
    [Column("TipID")]
    public int TipId { get; set; }

    [StringLength(15)]
    public string? Difficulty { get; set; }

    [StringLength(15)]
    public string? Topic { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Info { get; set; }

    [StringLength(70)]
    public string? VideoLink { get; set; }
}

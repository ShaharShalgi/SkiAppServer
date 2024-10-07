using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

[Table("Condition")]
public partial class Condition
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(10)]
    public string? StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

[Table("TypeUser")]
public partial class TypeUser
{
    [Key]
    [Column("TypeID")]
    public int TypeId { get; set; }

    [StringLength(15)]
    public string? TypeName { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<Professional> Proffesionals { get; set; } = new List<Professional>();
}

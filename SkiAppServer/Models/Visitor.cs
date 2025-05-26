using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class Visitor
{
    [Key]
    public int UserId { get; set; }

    [StringLength(25)]
    public string? Username { get; set; }

    [StringLength(25)]
    public string? Pass { get; set; }

    [StringLength(15)]
    public string? Gender { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    public bool? IsPro { get; set; }

    public bool? IsAdmin { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<PostPhoto> PostPhotos { get; set; } = new List<PostPhoto>();

    [InverseProperty("User")]
    public virtual Professional? Professional { get; set; }

    [InverseProperty("Reciever")]
    public virtual ICollection<Review> ReviewRecievers { get; set; } = new List<Review>();

    [InverseProperty("Sender")]
    public virtual ICollection<Review> ReviewSenders { get; set; } = new List<Review>();
}

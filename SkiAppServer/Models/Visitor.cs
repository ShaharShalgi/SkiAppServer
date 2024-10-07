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

    [InverseProperty("User")]
    public virtual Proffesional? Proffesional { get; set; }

    [InverseProperty("Reciever")]
    public virtual ICollection<Request> RequestRecievers { get; set; } = new List<Request>();

    [InverseProperty("Sender")]
    public virtual ICollection<Request> RequestSenders { get; set; } = new List<Request>();

    [InverseProperty("Reciever")]
    public virtual ICollection<Review> ReviewRecievers { get; set; } = new List<Review>();

    [InverseProperty("Sender")]
    public virtual ICollection<Review> ReviewSenders { get; set; } = new List<Review>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class Request
{
    [Key]
    [Column("RequestID")]
    public int RequestId { get; set; }

    [Column("SenderID")]
    public int? SenderId { get; set; }

    [Column("RecieverID")]
    public int? RecieverId { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [ForeignKey("RecieverId")]
    [InverseProperty("RequestRecievers")]
    public virtual Visitor? Reciever { get; set; }

    [ForeignKey("SenderId")]
    [InverseProperty("RequestSenders")]
    public virtual Visitor? Sender { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Requests")]
    public virtual Condition? Status { get; set; }
}

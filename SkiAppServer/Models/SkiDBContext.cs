using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SkiAppServer.Models;

public partial class SkiDBContext : DbContext
{
    public SkiDBContext()
    {
    }

    public SkiDBContext(DbContextOptions<SkiDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Proffesional> Proffesionals { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<TypeUser> TypeUsers { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=Ski_DB;User ID=AdminLogin;Password=shalgon101;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Conditio__C8EE20439969E473");
        });

        modelBuilder.Entity<Proffesional>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Proffesi__1788CCACC9D23457");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Type).WithMany(p => p.Proffesionals).HasConstraintName("FK__Proffesio__TypeI__29572725");

            entity.HasOne(d => d.User).WithOne(p => p.Proffesional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proffesio__UserI__286302EC");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519AACC9F1E2");

            entity.HasOne(d => d.Reciever).WithMany(p => p.RequestRecievers).HasConstraintName("FK__Requests__Reciev__33D4B598");

            entity.HasOne(d => d.Sender).WithMany(p => p.RequestSenders).HasConstraintName("FK__Requests__Sender__34C8D9D1");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests).HasConstraintName("FK__Requests__Status__35BCFE0A");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__REVIEWS__74BC79AEB7FF68B8");

            entity.HasOne(d => d.Reciever).WithMany(p => p.ReviewRecievers).HasConstraintName("FK__REVIEWS__Recieve__2E1BDC42");

            entity.HasOne(d => d.Sender).WithMany(p => p.ReviewSenders).HasConstraintName("FK__REVIEWS__SenderI__2F10007B");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1A897BC852E");
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TypeUser__516F0395BC7E1DDF");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Visitors__1788CC4C4771FB85");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

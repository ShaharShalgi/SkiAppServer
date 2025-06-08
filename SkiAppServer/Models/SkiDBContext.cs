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

    public virtual DbSet<ח> PostPhotos { get; set; }

    public virtual DbSet<Professional> Professionals { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewPhoto> ReviewPhotos { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<TypeUser> TypeUsers { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=Ski_DB;User ID=AdminLogin;Password=shalgon101;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ח>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__PostPhot__21B7B5E22B885F75");

            entity.HasOne(d => d.User).WithMany(p => p.PostPhotos).HasConstraintName("FK__PostPhoto__UserI__31EC6D26");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Professi__1788CCACA9894335");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Type).WithMany(p => p.Professionals).HasConstraintName("FK__Professio__TypeI__29572725");

            entity.HasOne(d => d.User).WithOne(p => p.Professional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profession__Link__286302EC");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__REVIEWS__74BC79AE65C0F112");

            entity.HasOne(d => d.Reciever).WithMany(p => p.ReviewRecievers).HasConstraintName("FK__REVIEWS__Recieve__2E1BDC42");

            entity.HasOne(d => d.Sender).WithMany(p => p.ReviewSenders).HasConstraintName("FK__REVIEWS__SenderI__2F10007B");
        });

        modelBuilder.Entity<ReviewPhoto>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__ReviewPh__21B7B5E2762F7AC6");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewPhotos).HasConstraintName("FK__ReviewPho__Revie__34C8D9D1");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1A8004DCF78");
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TypeUser__516F039503B104F3");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Visitors__1788CC4C7495C23B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

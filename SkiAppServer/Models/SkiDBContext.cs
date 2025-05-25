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

    public virtual DbSet<PostPhoto> PostPhotos { get; set; }

    public virtual DbSet<Professional> Professionals { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

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
        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Conditio__C8EE2043181592B1");
        });

        modelBuilder.Entity<PostPhoto>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__PostPhot__21B7B5E25D2C45ED");

            entity.HasOne(d => d.User).WithMany(p => p.PostPhotos).HasConstraintName("FK__PostPhoto__UserI__38996AB5");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Professi__1788CCACD0EEB8F9");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Type).WithMany(p => p.Professionals).HasConstraintName("FK__Professio__TypeI__29572725");

            entity.HasOne(d => d.User).WithOne(p => p.Professional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profession__Link__286302EC");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519A56CC83ED");

            entity.HasOne(d => d.Reciever).WithMany(p => p.RequestRecievers).HasConstraintName("FK__Requests__Reciev__33D4B598");

            entity.HasOne(d => d.Sender).WithMany(p => p.RequestSenders).HasConstraintName("FK__Requests__Sender__34C8D9D1");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests).HasConstraintName("FK__Requests__Status__35BCFE0A");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__REVIEWS__74BC79AE7612D7A8");

            entity.HasOne(d => d.Reciever).WithMany(p => p.ReviewRecievers).HasConstraintName("FK__REVIEWS__Recieve__2E1BDC42");

            entity.HasOne(d => d.Sender).WithMany(p => p.ReviewSenders).HasConstraintName("FK__REVIEWS__SenderI__2F10007B");
        });

        modelBuilder.Entity<ReviewPhoto>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__ReviewPh__21B7B5E2752CE495");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewPhotos).HasConstraintName("FK__ReviewPho__Revie__3B75D760");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1A8F96D5207");
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TypeUser__516F039560197F26");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Visitors__1788CC4C97A6B2D2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

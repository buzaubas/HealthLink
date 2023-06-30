using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthLink.API.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=HealthLink; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historie__3214EC07C157B1EC");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.NewStatus).WithMany(p => p.HistoryNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Histories__NewSt__48CFD27E");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.HistoryOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Histories__OldSt__47DBAE45");

            entity.HasOne(d => d.Request).WithMany(p => p.Histories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Histories__Reque__46E78A0C");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC0771C207BD");

            entity.HasOne(d => d.User).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__UserI__3E52440B");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC07C0747AD7");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateProcessed).HasColumnType("datetime");

            entity.HasOne(d => d.Organization).WithMany(p => p.Requests)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Organi__4222D4EF");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Status__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__412EB0B6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC075CE1ABE1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Statuses__3214EC07DA7853FD");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07B3EEFEBF");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OfficeDailyStatusTracker.Data.Tables;

public partial class OfficeStatusTrackerContext : DbContext
{
    public OfficeStatusTrackerContext()
    {
    }

    public OfficeStatusTrackerContext(DbContextOptions<OfficeStatusTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyStatus> DailyStatuses { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        // => optionsBuilder.UseSqlServer("Data Source=MS-NB0101;Initial Catalog=OfficeStatusTracker;Integrated Security=True;User ID=Mani;Password=Mani#MS;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DailySta__3214EC074F095857");

            entity.ToTable("DailyStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualTime)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Project)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Task)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LastLogin).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.UpdatedOn).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

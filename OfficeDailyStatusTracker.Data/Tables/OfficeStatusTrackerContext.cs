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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=MS-NB0101;Database=OfficeStatusTracker;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

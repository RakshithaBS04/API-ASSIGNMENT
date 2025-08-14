using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst.Models;

public partial class SqlpContext : DbContext
{
    public SqlpContext()
    {
    }

    public SqlpContext(DbContextOptions<SqlpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-00GRLI4;database=sqlp;integrated security=true;trustservercertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("PK__Bookings__C6D30705731AD437");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Bookingdate).HasColumnName("bookingdate");
            entity.Property(e => e.Facilityid).HasColumnName("facilityid");
            entity.Property(e => e.Memberid).HasColumnName("memberid");
            entity.Property(e => e.Slottime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("slottime");
            entity.Property(e => e.Trainerid).HasColumnName("trainerid");

            
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_DBFirst.Models;

public partial class WeatherForecastContext : DbContext
{
    public WeatherForecastContext()
    {
    }

    public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-00GRLI4;database=weather_forecast;integrated security=true;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__CITIES__6E64DFEA0E6BA363");

            entity.ToTable("CITIES");

            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("CITY_ID");
            entity.Property(e => e.CityName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY_NAME");
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

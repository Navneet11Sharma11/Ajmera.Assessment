using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ajmera.Assessment.DL.Models;

public partial class AjmeraContext : DbContext
{
    public AjmeraContext()
    {
    }

    public AjmeraContext(DbContextOptions<AjmeraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookMaster> BookMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)    
        //{    
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.    
        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookMaster>(entity =>
        {
            entity.HasKey(e => e.BookMasterID).HasName("PK__BookMast__3214EC0749AB89A0");

            entity.ToTable("BookMaster");

            entity.Property(e => e.BookMasterID).ValueGeneratedNever();
            entity.Property(e => e.AuthorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Ajmera;User ID=SA; Password=Password123;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookMaster>(entity =>
        {
            entity.HasKey(e => e.BookMasterId).HasName("PK__BookMast__B217DBD4306E9818");

            entity.ToTable("BookMaster");

            entity.Property(e => e.BookMasterId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("BookMasterID");
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

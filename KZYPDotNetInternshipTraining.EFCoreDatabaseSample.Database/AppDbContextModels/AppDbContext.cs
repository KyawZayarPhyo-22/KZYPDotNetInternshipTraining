using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=KZYPDotNetInternshipTraining;User Id=sa;Password=sasa@123;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E3087821966");

            entity.Property(e => e.BlogAuthor).HasMaxLength(100);
            entity.Property(e => e.BlogTitle).HasMaxLength(200);
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B990F489B09");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FatherName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.StudentName).HasMaxLength(100);
            entity.Property(e => e.StudentNo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

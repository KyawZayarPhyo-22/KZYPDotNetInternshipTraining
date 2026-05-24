using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KZYPDotNetInternshipTraining.EFCoreModelSample;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=KZYPDotNetInternshipTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");
        }
    }
    public DbSet<Student> Students { get; set; } = null!;
}
[Table("Tbl_Student")]
public class Student
{
    [Key]
    public int StudentId { get; set; }
    public string StudentNo { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public string FatherName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }


}
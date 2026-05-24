using KZYPDotNetInternshipTraining.EFCoreModelSample;
using Newtonsoft.Json;
using KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels;
using System.Text.Json.Serialization;
namespace KZYPDotNetInternshipTraining.EFCoreDatabaseSample;

public class EFCoreDatabaseSample
{
    private readonly KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels.AppDbContext _db;
    public EFCoreDatabaseSample()
    {
        _db = new KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels.AppDbContext();
    }
    public void Read()
    {
        List<TblStudent> lst = _db.TblStudents.ToList();
        foreach (TblStudent item in lst)
        {
            System.Console.WriteLine($"StudentId: {item.StudentId}, StudentNo: {item.StudentNo}, StudentName: {item.StudentName},FatherName: {item.FatherName}, Address: {item.Address}");
            System.Console.WriteLine("-----------------");
        }

    }
    public void Edit()
    {
        TblStudent item = _db.TblStudents.FirstOrDefault(x => x.StudentId == 17);
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        System.Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));



    }
    public void Create()
    {
        TblStudent student = new TblStudent()
        {
            StudentNo = "STU_008",
            StudentName = "Mg Nyi",
            FatherName = "U Lwin Lwin",
            Address = "Yangon",
            DateOfBirth = new DateTime(2000, 1, 1),
            CreatedDateTime = DateTime.Now,
            CreatedBy = "1"
        };
        _db.TblStudents.Add(student);
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Saving Successful" : "Saving Failed");


    }
    public void Update()
    {
        TblStudent student = _db.TblStudents.FirstOrDefault(x => x.StudentId == 17);
        if (student is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        student.StudentName = "Naing Linn Aung";
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Updating Successful" : "Updating Failed");

    }

    public void Delete()
    {
        TblStudent student = _db.TblStudents.FirstOrDefault(x => x.StudentId == 33);
        if (student is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        student.IsDelete = true;
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Deleting Successful" : "Deleting Failed");


    }

}
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace KZYPDotNetInternshipTraining.EFCoreModelSample;

public class EFCoreModelSample
{
    private readonly AppDbContext _db;
    public EFCoreModelSample()
    {
        _db = new AppDbContext();
    }
    public void Read()
    {
        List<Student> lst = _db.Students.ToList();
        foreach (Student item in lst)
        {
            System.Console.WriteLine($"StudentId: {item.StudentId}, StudentNo: {item.StudentNo}, StudentName: {item.StudentName},FatherName: {item.FatherName},Adderss: {item.Address}");
            System.Console.WriteLine(".........................");
        }

    }
    public void Edit()
    {
        Student item = _db.Students.FirstOrDefault(x => x.StudentId == 17);
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        System.Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));



    }
    public void Create()
    {
        Student student = new Student()
        {
            StudentNo = "STU1113",
            StudentName = "Mg Aung",
            FatherName = "U Kyaw",
            Address = "Yangon",
            DateOfBirth = new DateTime(2003, 6,2),
            CreatedDateTime = DateTime.Now,
            CreatedBy = "1"
        };
        _db.Students.Add(student);
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Saving Successful" : "Saving Failed");


    }
    public void Update()
    {
        Student student = _db.Students.FirstOrDefault(x => x.StudentId == 17);
        if (student is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        student.StudentName = "Tun Tun";
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Updating Successful" : "Updating Failed");

    }
    public void Delete()
    {
        Student student = _db.Students.FirstOrDefault(x => x.StudentId == 37);
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
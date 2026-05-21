using Dapper;
using KZYPDotNetInternshipTraining.DapperSample;
using Microsoft.Data.SqlClient;
using System.Data;


public class DapperSample
{
    private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "StudentsDb",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };
    public void Read()
    {
        string sql = @"SELECT TOP (1000) [StudentId]
      ,[StudentNumber]
      ,[StudentName]
      ,[FatherName]
      ,[Age]
      ,[Gender]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreateBy]
      ,[ModifiedDateTime]
      ,[ModifyBy]
  FROM [StudentsDb].[dbo].[Tbl_StudentInfo] Where IsDelete = 0";

        using IDbConnection sqlConnection = new SqlConnection(builder.ConnectionString);
        sqlConnection.Open();
        List<Student> lst = sqlConnection.Query<Student>(sql).ToList();
        foreach (Student item in lst)
        {
            System.Console.WriteLine($"StudentId: {item.StudentId}, StudentNumber: {item.StudentNumber}, StudentName: {item.StudentName},FatherName: {item.FatherName}");

        }
    }
    public void Edit()
    {
        string sql = $@"SELECT TOP (1000) [StudentId]
      ,[StudentNumber]
      ,[StudentName]
      ,[FatherName]
      ,[Age]
      ,[Gender]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreateBy]
      ,[ModifiedDateTime]
      ,[ModifyBy]
  FROM [StudentsDb].[dbo].[Tbl_StudentInfo] Where StudentId = @StudentId and IsDelete = 0";


        using IDbConnection sqlConnection = new SqlConnection(builder.ConnectionString);
        sqlConnection.Open();
        Student item = sqlConnection.Query<Student>(sql, new Student { StudentId = 17 }).FirstOrDefault();
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        System.Console.WriteLine($"StudentId: {item.StudentId}, StudentNumber: {item.StudentNumber}, StudentName: {item.StudentName},FatherName: {item.FatherName}");

    }
    // CREATE
    public void Create()
    {
        string sql = @"INSERT INTO Tbl_StudentInfo
                       (
                           StudentNumber,
                           StudentName,
                           FatherName,
                           Age,
                           Gender,
                           IsDelete,
                           CreatedDateTime,
                           CreateBy
                       )
                       VALUES
                       (
                           @StudentNumber,
                           @StudentName,
                           @FatherName,
                           @Age,
                           @Gender,
                           @IsDelete,
                           @CreatedDateTime,
                           @CreateBy
                       )";

        Student student = new Student()
        {
            StudentNumber = "STU089",
            StudentName = "Mg Mg",
            FatherName = "U Ba",
            Age = 20,
            Gender = "male",
            CreatedDateTime = DateTime.Now,
            CreateBy = "1"
        };

        using IDbConnection sqlConnection = new SqlConnection(builder.ConnectionString);
        sqlConnection.Open();

        int result = sqlConnection.Execute(sql, student);

        Console.WriteLine(result > 0 ? "Saving Successful" : "Saving Failed");
    }
    public void Update()
    {
        string sql = @"UPDATE Tbl_StudentInfo
                       SET
                           StudentNumber = @StudentNumber,
                           StudentName = @StudentName,
                           FatherName = @FatherName,
                           Age = @Age,
                           Gender = @Gender,
                           ModifiedDateTime = @ModifiedDateTime,
                           ModifyBy = @ModifyBy
                       WHERE StudentId = @StudentId
                       AND IsDelete = 0";

        Student student = new Student()
        {
            StudentId = 20,
            StudentNumber = "S-016",
            StudentName = "Aung Aung",
            FatherName = "U Tun",
            Age = 21,
            Gender = "male",
            ModifiedDateTime = DateTime.Now,
            ModifyBy = "Admin",

        };

        using IDbConnection sqlConnection = new SqlConnection(builder.ConnectionString);
        sqlConnection.Open();

        int result = sqlConnection.Execute(sql, student);

        Console.WriteLine(result > 0 ? "Updating Successful" : "Updating Failed");
    }

    // DELETE (Soft Delete)
    public void Delete()
    {
        string sql = @"UPDATE Tbl_StudentInfo
                       SET
                           IsDelete = 1,
                           ModifiedDateTime = @ModifiedDateTime,
                           ModifyBy = @ModifyBy
                       WHERE StudentId = @StudentId";

        var student = new
        {
            StudentId = 16,
            ModifiedDateTime = DateTime.Now,
            ModifyBy = "Admin"
        };

        using IDbConnection sqlConnection = new SqlConnection(builder.ConnectionString);
        sqlConnection.Open();

        int result = sqlConnection.Execute(sql, student);

        Console.WriteLine(result > 0 ? "Deleting Successful" : "Deleting Failed");
    }
}


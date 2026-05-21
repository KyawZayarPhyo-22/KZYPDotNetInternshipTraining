using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZYPDotNetInternshipTraining.AdoDotNetSample
{

    public class AdoDotNetSample
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

            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"SELECT [StudentId]
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
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            List<Student> lst = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                Student item = new Student()
                {
                    StudentId = Convert.ToInt32(row["StudentId"]),
                    StudentNumber = row["StudentNumber"].ToString()!,
                    StudentName = row["StudentName"].ToString()!,
                    FatherName = row["FatherName"].ToString()!,
                    IsDelete = Convert.ToBoolean(row["IsDelete"]),
                    CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                    CreateBy = row["CreateBy"].ToString()!,
                    ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                    ModifyBy = row["ModifyBy"] == DBNull.Value ? null : row["ModifyBy"].ToString()
                };
                lst.Add(item);

                Console.WriteLine($"{item.StudentNumber},{item.StudentName},{item.FatherName},{item.Age},{item.Gender}");
            }
        }

        public void Edit()
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            int id = 5;
            string query = $@"SELECT[StudentId]
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
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No record found.");
                return;
            }

            DataRow row = dt.Rows[0];

            Student item = new Student()
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                StudentNumber = row["StudentNumber"].ToString()!,
                StudentName = row["StudentName"].ToString()!,
                FatherName = row["FatherName"].ToString()!,
                Age = Convert.ToInt32(row["Age"]),
                Gender = row["Gender"].ToString()!,
                IsDelete = Convert.ToBoolean(row["IsDelete"]),
                CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                CreateBy = row["CreateBy"].ToString()!,
                ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                ModifyBy = row["ModifyBy"] == DBNull.Value ? null : row["ModifyBy"].ToString()
            };

            Console.WriteLine($"Edit {item.StudentName}");

        }

        public void Create()
        {
            Student item = new Student()
            {
                StudentNumber = "STU016",
                StudentName = "Aung",
                FatherName = "U Haha",
                Age = 20,
                Gender = "male",
                IsDelete = false,
                CreatedDateTime = DateTime.Now,
                CreateBy = "admin"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_StudentInfo]
                (
                    [StudentNumber],
                    [StudentName],
                    [FatherName],
                    [Age],
                    [Gender],
                    [IsDelete],
                    [CreatedDateTime]
                )
                VALUES
                (
                    @StudentNumber,
                    @StudentName,
                    @FatherName,
                    @Age,
                    @Gender,
                    @IsDelete,
                    @CreatedDateTime
                )";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentNumber", item.StudentNumber);
            cmd.Parameters.AddWithValue("@StudentName", item.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", item.FatherName);
            cmd.Parameters.AddWithValue("@Age", item.Age);
            cmd.Parameters.AddWithValue("@Gender", item.Gender);
            cmd.Parameters.AddWithValue("@IsDelete", item.IsDelete);
            cmd.Parameters.AddWithValue("@CreatedDateTime", item.CreatedDateTime);
            int result = cmd.ExecuteNonQuery();

            Console.WriteLine(result > 0 ? "Create successful." : "Create failed.");
        }

        public void Update()
        {
            Student item = new Student()
            {
                StudentId = 1,
                StudentNumber = "STU001",
                StudentName = "Aung Aung Updated",
                FatherName = "U Hla Myint",
                Age = 20,
                Gender = "male",
                IsDelete = false,
                ModifiedDateTime = DateTime.Now,
                ModifyBy = "admin"
            };


            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"
UPDATE [dbo].[Tbl_StudentInfo]
SET
    [StudentNumber] = @StudentNumber,
    [StudentName] = @StudentName,
    [FatherName] = @FatherName,
    [Age] = @Age,
    [Gender] = @Gender,
    [IsDelete] = @IsDelete,
    [ModifiedDateTime] = @ModifiedDateTime,
    [ModifyBy] = @ModifyBy
WHERE [StudentId] = @StudentId";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", item.StudentId);
            cmd.Parameters.AddWithValue("@StudentNumber", item.StudentNumber);
            cmd.Parameters.AddWithValue("@StudentName", item.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", item.FatherName);
            cmd.Parameters.AddWithValue("@Age", item.Age);
            cmd.Parameters.AddWithValue("@Gender", item.Gender);
            cmd.Parameters.AddWithValue("@IsDelete", item.IsDelete);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", item.ModifiedDateTime);
            cmd.Parameters.AddWithValue("@ModifyBy", item.ModifyBy);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine(result > 0 ? "Update successful." : "Update failed.");
        }

        public void Delete()
        {
            int id = 1;

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"
UPDATE [dbo].[Tbl_StudentInfo]
SET
    [IsDelete] = @IsDelete,
    [ModifiedDateTime] = @ModifiedDateTime,
    [ModifyBy] = @ModifyBy
WHERE [StudentId] = @StudentId";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@IsDelete", true);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifyBy", "admin");

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine(result > 0 ? "Delete successful." : "Delete failed.");
        }

    }
}

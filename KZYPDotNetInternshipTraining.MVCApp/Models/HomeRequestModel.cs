namespace KZYPDotNetInternshipTraining.MVCApp.Models
{
    public class HomeRequestModel


    {
        public int Id { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class HomeResponseModel
    {
        public int Id { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    

    public class StudentResponseModel
    {
        public List<StudentModel> Data { get; set; }
        public int PageCount { get; set; }
        public int TotalRecords { get; set; }
        public int PageNo { get; set; } 
        public int PageSize { get; set; }
    }

    public class StudentRequestModel
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10; 

    }
    public class StudentModel
    {
        public int Id { get; set; }

        public string No { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedDateTime { get; set; }

        public string? ModifiedBy { get; set; }
    }

    public class CreateStudentRequestModel
    {
        public string No { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FatherName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateStudentRequestModel
    {
        public StudentModel Data { get; set; }

    }
    public class EditStudentRequestModel
    {
        public int Id { get; set; }
        public string No { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FatherName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
    public class DeleteStudentRequestModel
    {
        public int Id { get; set; }
    }
}

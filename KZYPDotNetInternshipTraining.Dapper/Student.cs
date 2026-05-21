using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZYPDotNetInternshipTraining.DapperSample
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public bool IsDelete { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime? ModifiedDateTime { get; set; }
        public string? ModifyBy { get; set; }
    }
}

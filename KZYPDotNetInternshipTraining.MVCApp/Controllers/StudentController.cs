using KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels;
using Microsoft.AspNetCore.Mvc;
using KZYPDotNetInternshipTraining.MVCApp.Models;
using Microsoft.EntityFrameworkCore;
namespace KZYPDotNetInternshipTraining.MVCApp.Controllers
{
    public class StudentController : Controller

    {
        private readonly AppDbContext _db;

        public StudentController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync([FromQuery]StudentRequestModel requestModel)
        {
            var query = _db.TblStudents.AsQueryable();
            int rowCount= await query.CountAsync();
            int PageNo = requestModel.PageNo;
            int PageSize = requestModel.PageSize;
            var lst= query
                .OrderByDescending(x => x.StudentId)
                .Skip((PageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            StudentResponseModel model = new StudentResponseModel();
            model.PageCount= rowCount / PageSize;
            if (rowCount % PageSize > 0)
            {
                model.PageCount++;
            }
            model.TotalRecords = rowCount;
            model.PageSize = PageSize;
            model.PageNo = PageNo;
            model.Data = lst.Select(x => new StudentModel
            {
                Id = x.StudentId,
                No = x.StudentNo,
                Name = x.StudentName,
                FatherName = x.FatherName,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                IsDelete = x.IsDelete,
                CreatedDateTime =  x.CreatedDateTime,
                CreatedBy = x.CreatedBy,
                ModifiedDateTime = x.ModifiedDateTime,
                ModifiedBy = x.ModifiedBy
            }).ToList();
            return View(model) ;
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateStudentRequestModel requestModel)
        {
            _db.TblStudents.Add(new TblStudent
            {
                StudentNo = requestModel.No,
                StudentName = requestModel.Name,
                FatherName = requestModel.FatherName,
                Address = requestModel.Address,
                DateOfBirth = requestModel.DateOfBirth,
                IsDelete = false,
                CreatedDateTime = DateTime.Now,
                CreatedBy = "Admin"
            });
            var result = await _db.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] =result > 0 ? "Student created successfully." : "Failed";
            return Redirect("/student");
        }
    }
}
 
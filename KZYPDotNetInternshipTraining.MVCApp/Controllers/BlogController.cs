using KZYPDotNetInternshipTraining.EFCoreDatabaseSample.Database.AppDbContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KZYPDotNetInternshipTraining.MVCApp.Models;


namespace KZYPDotNetInternshipTraining.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index([FromQuery]BlogsModelRequest BlogRequest)
        {
            int pageNo = BlogRequest.PageNo <= 0 ? 1 : BlogRequest.PageNo;
            int pageSize = BlogRequest.PageSize <= 0 ? 5 : BlogRequest.PageSize;
            int totalRecords = _db.TblBlogs.Count();
            var blogsResponseList = _db.TblBlogs
            .OrderBy(b => b.BlogId) 
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BlogsModelResponse
            {
                Id = b.BlogId,
                BlogTitle = b.BlogTitle,
                BlogAuthor = b.BlogAuthor,
                BlogContent = b.BlogContent
            }).ToList();

          
            var viewModel = new BlogListViewModel
            {
                Blogs = blogsResponseList,
                CurrentPage = pageNo,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogsModelRequest requestModel)
        {
            _db.TblBlogs.Add(new TblBlog
            {
                BlogTitle = requestModel.BlogTitle,
                BlogAuthor = requestModel.BlogAuthor,
                BlogContent = requestModel.BlogContent
            });
            var result = await _db.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Blog created successfully." : "Failed to create blog.";

            return Redirect("/blog");
        }

        public async Task<IActionResult> Edit([FromQuery] BlogEditRequestModel requestModel)
        {
            var item = await _db.TblBlogs.FirstOrDefaultAsync(x => x.BlogId == requestModel.Id);
            if (item is null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Blog not found.";
                return Redirect("/blog");
            }

            BlogEditResponseModel model = new BlogEditResponseModel
            {
                Data = new BlogModel
                {
                    Author = item.BlogAuthor,
                    Content = item.BlogContent,
                    Title = item.BlogTitle,
                    Id = item.BlogId
                }
            };
            return View(model);
        }

        #region Update Blog

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateRequestModel requestModel)
        {
            var item = await _db.TblBlogs.FirstOrDefaultAsync(x => x.BlogId == requestModel.Id);
            if (item is null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Blog not found.";
                return Redirect("/blog");
            }


            item.BlogTitle = requestModel.Title;
            item.BlogAuthor = requestModel.Author;
            item.BlogContent = requestModel.Content;

            _db.TblBlogs.Update(item);
            var result = await _db.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Blog updated successfully." : "Failed to update blog.";

            return Redirect("/blog");
        }

        #endregion

        #region Delete Blog (Yes / No Confirmation Screen)
        public async Task<IActionResult> Delete([FromQuery] BlogEditRequestModel requestModel)
        {
            var item = await _db.TblBlogs.FirstOrDefaultAsync(x => x.BlogId == requestModel.Id);
            if (item is null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Blog not found.";
                return Redirect("/blog");
            }

            BlogEditResponseModel model = new BlogEditResponseModel
            {
                Data = new BlogModel
                {
                    Id = item.BlogId,
                    Title = item.BlogTitle,
                    Author = item.BlogAuthor,
                    Content = item.BlogContent
                }
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var item = await _db.TblBlogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Blog not found.";
                return Redirect("/blog");
            }

            _db.TblBlogs.Remove(item);
            var result = await _db.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Blog deleted successfully." : "Failed to delete blog.";

            return Redirect("/blog");
        }
    }
}

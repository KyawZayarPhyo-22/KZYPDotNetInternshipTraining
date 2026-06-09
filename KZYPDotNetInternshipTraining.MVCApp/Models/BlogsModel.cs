namespace KZYPDotNetInternshipTraining.MVCApp.Models
{
    public class BlogsModelRequest
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public int PageNo { get; set; } = 1;     
        public int PageSize { get; set; } = 5;
    }

    public class BlogsModelResponse
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }
    public class BlogListViewModel
    {
        public List<BlogsModelResponse> Blogs { get; set; } = new();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }

    public class BlogEditRequestModel
    {
        public int Id { get; set; }
    }

    public class BlogEditResponseModel
    {
        public BlogModel Data { get; set; }
    }

    public class BlogModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Content { get; set; } = null!;
    }

    public class BlogCreateRequestModel
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Content { get; set; } = null!;
    }

    public class BlogCreateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class BlogUpdateRequestModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

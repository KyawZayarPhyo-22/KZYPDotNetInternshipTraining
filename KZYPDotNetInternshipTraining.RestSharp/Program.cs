using Newtonsoft.Json;
using RestSharp;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

RestClient client = new RestClient();
string baseUrl = "https://localhost:7258/api/blog";

await Read();
await Create();
await Edit();
await Patch();
await Delete();

Console.ReadLine();

async Task Read()
{
    RestRequest request = new RestRequest(baseUrl, Method.Get);
    var response = await client.ExecuteAsync(request);

    if (response.IsSuccessStatusCode)
    {
        var content = response.Content;
        Console.WriteLine("Read Result:");
        Console.WriteLine(content);
    }
}

async Task Create()
{
    var requestModel = new BlogCreateRequestModel
    {
        BlogTitle = "Blog Title 1",
        BlogAuthor = "Blog Author 1",
        BlogContent = "Blog Content 1"
    };

    RestRequest request = new RestRequest(baseUrl, Method.Post);
    request.AddJsonBody(requestModel);


    var response = await client.ExecuteAsync(request);

    var content = response.Content;
    Console.WriteLine("Create Result:");
    Console.WriteLine(content);
}

async Task Edit()
{
    int id = 1;

    var requestModel = new BlogUpdateRequestModel
    {
        BlogTitle = "Updated Blog Title",
        BlogAuthor = "Updated Blog Author",
        BlogContent = "Updated Blog Content"
    };

    RestRequest request = new RestRequest($"{baseUrl}/{id}", Method.Put);
    request.AddJsonBody(requestModel);

    var response = await client.ExecuteAsync(request);

    Console.WriteLine("Edit Result:");
    Console.WriteLine(response.Content);
}

async Task Patch()
{
    int id = 1;

    var requestModel = new BlogPatchRequestModel
    {
        BlogTitle = "Patch Blog Title"
    };

    RestRequest request = new RestRequest($"{baseUrl}/{id}", Method.Patch);
    request.AddJsonBody(requestModel);

    var response = await client.ExecuteAsync(request);

    Console.WriteLine("Patch Result:");
    Console.WriteLine(response.Content);
}

async Task Delete()
{
    int id = 1;

    RestRequest request = new RestRequest($"{baseUrl}/{id}", Method.Delete);

    var response = await client.ExecuteAsync(request);

    Console.WriteLine("Delete Result:");
    Console.WriteLine(response.Content);
}

public class BlogCreateRequestModel
{
    public string BlogTitle { get; set; } = null!;
    public string BlogAuthor { get; set; } = null!;
    public string BlogContent { get; set; } = null!;
}

public class BlogUpdateRequestModel
{
    public string BlogTitle { get; set; } = null!;
    public string BlogAuthor { get; set; } = null!;
    public string BlogContent { get; set; } = null!;
}

public class BlogPatchRequestModel
{
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}
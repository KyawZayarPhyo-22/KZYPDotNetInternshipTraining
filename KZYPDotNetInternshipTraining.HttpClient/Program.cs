using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

HttpClient client = new HttpClient();
string baseUrl = "https://localhost:7258/api/blog";

await Read();
await Create();
await Edit();
await Patch();
await Delete();

Console.ReadLine();

async Task Read()
{
    var response = await client.GetAsync(baseUrl);

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
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

    string jsonRequest = JsonConvert.SerializeObject(requestModel);
    var stringContent = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);

    var response = await client.PostAsync(baseUrl, stringContent);

    var content = await response.Content.ReadAsStringAsync();
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

    string jsonRequest = JsonConvert.SerializeObject(requestModel);
    var stringContent = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);

    var response = await client.PutAsync($"{baseUrl}/{id}", stringContent);

    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Edit Result:");
    Console.WriteLine(content);
}

async Task Patch()
{
    int id = 1;

    var requestModel = new BlogPatchRequestModel
    {
        BlogTitle = "Patch Blog Title"
    };

    string jsonRequest = JsonConvert.SerializeObject(requestModel);
    var stringContent = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);

    var response = await client.PatchAsync($"{baseUrl}/{id}", stringContent);

    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Patch Result:");
    Console.WriteLine(content);
}

async Task Delete()
{
    int id = 1;

    var response = await client.DeleteAsync($"{baseUrl}/{id}");

    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Delete Result:");
    Console.WriteLine(content);
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
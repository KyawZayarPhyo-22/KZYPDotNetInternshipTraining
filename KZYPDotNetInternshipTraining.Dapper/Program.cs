// See https://aka.ms/new-console-template for more information
using KZYPDotNetInternshipTraining.DapperSample;

class Program
{
    static void Main(string[] args)
    {
        DapperSample dapper = new DapperSample();
        dapper.Create();
        dapper.Edit();
        dapper.Read();
        dapper.Update();
        dapper.Delete();
    }
}
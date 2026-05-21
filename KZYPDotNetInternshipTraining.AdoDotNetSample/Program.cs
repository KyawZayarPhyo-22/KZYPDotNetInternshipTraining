// See https://aka.ms/new-console-template for more information
using KZYPDotNetInternshipTraining.AdoDotNetSample;
using Microsoft.Data.SqlClient;
using System.Data;

class program
{
    static void Main(string[] args)
    {
        AdoDotNetSample adoDotNetSample = new AdoDotNetSample();
        adoDotNetSample.Create();
        adoDotNetSample.Edit();
        adoDotNetSample.Read();
        adoDotNetSample.Update();
        adoDotNetSample.Delete();

    }
}
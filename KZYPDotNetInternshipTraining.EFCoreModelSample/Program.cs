// See https://aka.ms/new-console-template for more information
using KZYPDotNetInternshipTraining.EFCoreModelSample;
class Prgram
{
    static void Main(string[] args)
    {
        EFCoreModelSample modelsample = new EFCoreModelSample();
        modelsample.Create();
        modelsample.Edit();
        modelsample.Update();
        modelsample.Delete();
        modelsample.Read();
    }
}
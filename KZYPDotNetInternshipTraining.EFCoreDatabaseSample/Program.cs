using KZYPDotNetInternshipTraining.EFCoreDatabaseSample;
class Program
{
    static void Main(string[] args)
    {
        EFCoreDatabaseSample sample = new EFCoreDatabaseSample();
        sample.Edit();
        sample.Create();
        sample.Update();
        sample.Delete();
        sample.Read();
    }
}
using Esi_BusinessLayer.Rules;
using System;

namespace _3esi_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileUpload = @"UploadedFiles\3esi.csv";
            Console.WriteLine("Uploading Data");
            UploadData udClass = new UploadData();
            udClass.Execute(FileUpload);

            //display successfull and failed lists
            udClass.LoadDisplayErrors();
            //DisplayGroupsAndChildrenWells(groupsDictionary);
            //DisplayStandAloneWells(wellsList);

            Console.ReadLine();
        }
    }
}

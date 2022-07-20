using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Backup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Backup deserialization = new Backup();
            string Original_Path = deserialization.Desser_Json().Original_Path;
            string Target_Path = deserialization.Desser_Json().Target_Path;
            Console.WriteLine($"Исходная папка: {Original_Path}\nЦелевая папка: {Target_Path} ");
            //deserialization.CopyFolder(Original_Path, Target_Path);
            deserialization.Show_Info_Directory(deserialization.Desser_Json());
        }
    }
}

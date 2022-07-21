using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;




namespace File_Backup
{
    internal class Backup
    {
        public string Original_Path { get; set; }
        public string Target_Path { get; set; }
        public Backup( )
        {  
        }
        public Backup(string Original_Path, string Target_Path)
        {
            this.Original_Path = Original_Path;
            this.Target_Path = Target_Path;
        }
        public void Serial_Json()
        {
            string path = $@"E:\copy_path.json";
            Backup serializ = new Backup($@"E:\Original_Path", $@"E:\Target_Path");
            string json = JsonConvert.SerializeObject(serializ);
            File.WriteAllText(path, json);
            Console.WriteLine("Сериализация завершена");
            Console.ReadKey();
        }
        public Backup Desser_Json()
        {
          
            string path = $@"copy_path.json";
            string read_json = File.ReadAllText(path);
            Backup deserialization = JsonConvert.DeserializeObject<Backup>(read_json);
            
           // Console.WriteLine($"Original_Path = {deserialization.Original_Path}\nTarget_Path = {deserialization.Target_Path} ");
            //Console.ReadKey();
            return deserialization;
            


        }
       public void CopyFolder(string Original_Path, string Target_Path)
        {

            if (!Directory.Exists(Target_Path))
            {
                Directory.CreateDirectory(Target_Path);
            }

            string[] files = Directory.GetFiles(Original_Path);

            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(Target_Path, Path.GetFileName(file)), true);
            }

            string[] folders = Directory.GetDirectories(Original_Path);

            foreach (string folder in folders)
            {
                CopyFolder(folder, Path.Combine(Target_Path, Path.GetFileName(folder)));
            }
        }
        
        public void Show_Info_Directory(Backup deserialization)
        {
            string Original_Path = deserialization.Desser_Json().Original_Path;
            string Target_Path = deserialization.Desser_Json().Target_Path;
            // рекурсивно получаем перечнь всех папок
            string[] files_original = Directory.GetDirectories(Original_Path, "*.*", SearchOption.AllDirectories);
            string[] files_target = new string[files_original.Length];

            //создаем пустые папки для записи в них файлов
            for (int i = 0; i < files_target.Length; i++)
            {
                files_target[i] = files_original[i].Replace(Original_Path, Target_Path);
                if (Directory.Exists(files_target[i]) == false)
                {
                    Directory.CreateDirectory(files_target[i]);
                }
            }
            // проверяем  корневую папку для таргет файлов, если не существует создаем
            if (Directory.Exists(Target_Path) == false)
            {
                Directory.CreateDirectory(Target_Path);
            }

            // копируем файлы в корневой директории
            string[] fiels_root = Directory.GetFiles(Original_Path);

            foreach (string fiel in fiels_root)
            {
                File.Copy(fiel, Path.Combine(Target_Path, Path.GetFileName(fiel)), true);
            }

            // копируем файлы из вложеных папок в уже готовые таргет папки в нужном месте
            for (int i = 0; i < files_original.Length; i++)
            {
                string[] fiels = Directory.GetFiles(files_original[i]);

                foreach (string fiel in fiels)
                {
                    File.Copy(fiel, Path.Combine(files_target[i], Path.GetFileName(fiel)), true);
                }
            }
            //foreach (string file in files_original)
            //{
            //    Console.WriteLine(file);
            //}

            //string[] AllFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            //foreach (string filename in AllFiles)
            //{
            //    Console.WriteLine(filename);
            //}
            //for (int i = 0; i < AllFiles.Length; i++)
            //{
            //    FileInfo fileInfo = new FileInfo(AllFiles[i]);
            //    fileInfo.CopyTo(AllFiles[i]);
            //}

            //FileInfo fi = new FileInfo(@"E:\Greatest Hits 2020");
            //DirectoryInfo di = fi.Directory;
            //FileSystemInfo[] fsi = di.GetFileSystemInfos();
            //foreach (FileSystemInfo info in fsi)
            //    Console.WriteLine(info.Name);


            Console.ReadKey();
        }
        public void Zip(Backup deserialization)
        {
            string Original_Path = deserialization.Desser_Json().Original_Path;
            string date_time = DateTime.Now.ToString(("MM-dd-yyyy--HH-mm-ss"));
            string Target_Path = deserialization.Desser_Json().Target_Path + "\\" + date_time;
            string message = "Запущенно резервное копирование!";


            if (Directory.Exists(Original_Path) == false)
            {   
                Directory.CreateDirectory(Original_Path);
                 message =$"Созданна исходная папка {Original_Path}.";
            }

            if (Directory.Exists(Target_Path) == false)
            {
                Directory.CreateDirectory(Target_Path);
                 message = $"Созданна целевая папка {Target_Path + "\\Backup.zip"}.";
            }
           
            ZipFile.CreateFromDirectory(Original_Path, Target_Path + "\\Backup.zip");
            Console.WriteLine($"Папка {Original_Path} архивирована в файл {Target_Path + "\\Backup.zip"}.");
            Console.ReadKey();
        }
    }
}

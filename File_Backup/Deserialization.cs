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
    public  class Backup
    {
        public string Original_Path { get; set; }
        public string Target_Path { get; set; }
        public string Type_Loggers { get; set; }

        public Backup( )
        {  
        }
        public Backup(string Original_Path, string Target_Path, string Type_Loggers)
        {
            this.Original_Path = Original_Path;
            this.Target_Path = Target_Path;
            this.Type_Loggers = Type_Loggers;
        }
        //public void Serial_Json()
        //{
        //    string path = $@"E:\copy_path.json";
        //    Backup serializ = new Backup($@"E:\Original_Path", $@"E:\Target_Path");
        //    string json = JsonConvert.SerializeObject(serializ);
        //    File.WriteAllText(path, json);
        //    Console.WriteLine("Сериализация завершена");
        //    Console.ReadKey();
        //}
        public Backup Desser_Json()
        {
          
            string path = $@"copy_path.json";
            string read_json = File.ReadAllText(path);
            Backup deserialization = JsonConvert.DeserializeObject<Backup>(read_json);
            return deserialization;
        }
       public void CopyFolder(string Original_Path, string Target_Path )
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
           
            Console.ReadKey();
        }
        
         
        
        public void Zip(Backup deserialization)
        {
            //Настройка логгера
            Log_Input log_input = new Log_Input();
            string message = string.Empty;
            log_input.Output_console(true);
            log_input.Output_file(true);
            Type_Input_Loggers type_Input_Loggers;
            string date_time = DateTime.Now.ToString(("MM-dd-yyyy--HH-mm-ss"));
            string logs_path = deserialization.Target_Path + "\\" + "Logs" + "\\" + date_time + $@"_logs.txt";
            switch (deserialization.Type_Loggers)
            {
                case "Debug":
                      type_Input_Loggers = Type_Input_Loggers.Debug;
                        break;
                case "Info":
                    type_Input_Loggers = Type_Input_Loggers.Debug;
                    break;
                case "Error":
                    type_Input_Loggers = Type_Input_Loggers.Debug;
                    break;
                default: type_Input_Loggers = Type_Input_Loggers.Info;
                    break;


            }
            log_input.Type_Loggers(type_Input_Loggers);

            //
            try {
            string Original_Path = deserialization.Original_Path;
              
 
             string Target_Path = deserialization.Target_Path + "\\" + date_time;
                using (FileStream fs = new FileStream(logs_path, FileMode.Create))
                {
                   
                }


                message = "Запущенно резервное копирование! Дождись окончания процесса.";
                log_input.Info(message, logs_path);

            if (Directory.Exists(Original_Path) == false)
            {   
                Directory.CreateDirectory(Original_Path);
                message =$"Созданна исходная папка {Original_Path}.";
                log_input.Debug(message, logs_path);
            }

            if (Directory.Exists(Target_Path) == false)
            {
                Directory.CreateDirectory(Target_Path);
                message = $"Созданна целевая папка {Target_Path + "\\Backup.zip"}.";
                log_input.Debug(message, logs_path);
            }
           
            ZipFile.CreateFromDirectory(Original_Path, Target_Path + "\\Backup.zip");
            message = $"Папка {Original_Path} архивирована в файл {Target_Path + "\\Backup.zip"}.";
            log_input.Info(message, logs_path);
            Console.WriteLine($"Для выхода из программы резервного копирования нажмите пробел.");
            Console.ReadKey();
                
            }
            catch (Exception ex)
            {
                message = $"В методе {ex.TargetSite}, произошла ошибка - {ex.Message}\nОшибка вызвана в {ex.InnerException}.";
                log_input.Error(message, logs_path);
            }
        }
    }
}

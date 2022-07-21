using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace File_Backup
{

    public enum Type_Input_Loggers : byte
    {
        Info = 1,
        Debug,
        Error
    }

    ///
    public class Log_Input
    {   /// <summary>
        /// настроечные данные вывода
        /// </summary>
        /// 

        bool output_console;
        bool output_file;
       
        Type_Input_Loggers type_Input_Loggers;

        /// <summary>
        /// Вывод логов в консоль, true - если надо вывести
        /// </summary>
        /// <param name="Output_console"></param>
        public void Output_console(bool output_console)
        {
            this.output_console = output_console;
        }
        /// <summary>
        /// Вывод логов в текстовый файл, true - если надо вывести
        /// </summary>
        /// <param name="Output_file"></param>
        public void Output_file(bool output_file)
        {
            this.output_file = output_file;
        }
       
        /// <summary>
        /// Выбор уровня логирования
        /// </summary>
        /// <param name="type_Input_Loggers"></param>

        public void Type_Loggers(Type_Input_Loggers type_Input_Loggers)
        {
            this.type_Input_Loggers = type_Input_Loggers;
        }


        /// <summary>
        /// настроечне данные типа логов
        /// </summary>

        /// /////////////

        private DateTime dateTime;
        private string message;
        private string type_log;
        public Log_Input() { }
        public Log_Input(Type_Input_Loggers type_Input_Loggers, string message)
        {
            this.type_Input_Loggers = type_Input_Loggers;
            this.message = message;
        }
        public Log_Input(string type_log, DateTime dateTime, string message)
        {
            this.dateTime = dateTime;
            this.message = message;
        }
        public Log_Input(Type_Input_Loggers type_Input_Loggers, DateTime dateTime, string message)
        {
            this.type_Input_Loggers = type_Input_Loggers;
            this.dateTime = dateTime;
            this.message = message;
        }



        /// <summary>
        /// вывод логов в текстовый файл
        /// </summary>
        /// <param name="log"></param>
        void Output_File(Log_Input log)
        {
            Backup deser = new Backup();
            string str = deser.Desser_Json().Target_Path + "\\" + "Logs";
            string date_time = DateTime.Now.ToString(("MM-dd-yyyy--HH-mm-ss"));
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            string path = str + "\\" + date_time+ $@"logger.txt";


            File.AppendAllText(path, "\r\n  " + String.Concat(DateTime.Now.ToString() + "\t", log.message), Encoding.UTF8);
        }
        void Output_File(Log_Input log, string logs_path)
        {

            File.AppendAllText(logs_path, "\r\n  " + String.Concat(DateTime.Now.ToString() + "\t", log.message), Encoding.UTF8);
        }
        /// <summary>
        /// Вывод на экран
        /// </summary>
        /// <param name="str"></param>
        void Print(bool output_console, bool output_file,  string message, string logs_path, string type_log)
        {
            if (output_console == true) Console.WriteLine($"{DateTime.Now}   {message}");

            Log_Input log_output = new Log_Input(type_log, DateTime.Now, message);
            log_output.type_log = type_log;
            if (output_file == true) Output_File(log_output, logs_path);
             
        }
        void Print(bool output_console, bool output_file, string message, string type_log)
        {
            if (output_console == true) Console.WriteLine($"{DateTime.Now}   {message}");

            Log_Input log_output = new Log_Input(type_log, DateTime.Now, message);
            log_output.type_log = type_log;
            if (output_file == true) Output_File(log_output);

        }
        /// <summary>
        /// Фильтрация логов по двум атрибутам испоьзуется только для перегрузки с двумя атрибутами
        /// </summary>
        /// <param name="Switch"></param>
        public void Switch_logger(Log_Input log)
        {
            Log_Input log_input = new Log_Input();
            switch (log.type_Input_Loggers)
            {
                case Type_Input_Loggers.Info:
                    log_input.Info(log.type_Input_Loggers, log.message);
                    log_input.Error(log.type_Input_Loggers, log.message);
                    break;

                case Type_Input_Loggers.Debug:
                    log_input.Debug(log.type_Input_Loggers, log.message);
                    log_input.Info(log.type_Input_Loggers, log.message);
                    log_input.Error(log.type_Input_Loggers, log.message);
                    break;
                case Type_Input_Loggers.Error:
                    log_input.Error(log.type_Input_Loggers, log.message);
                    break;
                default:
                    type_Input_Loggers = Type_Input_Loggers.Info;

                    break;
            }
        }

        /// <summary>
        /// Перегрузка метода  Info с двумя атрибутами
        /// </summary>
        /// <param name="Info"></param>
        /// 

        public void Info(Type_Input_Loggers type_Input_Loggers, string message)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Info || type_Input_Loggers == Type_Input_Loggers.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                //log_Input.type_log
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Info";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, type_log);
            }
        }
        /// <summary>
        /// Перегрузка метода  Info с одним атрибутом
        /// </summary>
        /// <param name="Info"></param>
        public void Info(string message, string logs_path)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Info || type_Input_Loggers == Type_Input_Loggers.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Info";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, logs_path, type_log);
            }
        }

        /// <summary>
        /// Перегрузка метода  Debug с двумя атрибутами
        /// </summary>
        /// <param name="Debug"></param>
        public void Debug(Type_Input_Loggers type_Input_Loggers, string message)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Debug";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, type_log);
            }
        }
        /// <summary>
        /// Перегрузка метода  Debug с одним атрибутом
        /// </summary>
        /// <param name="Debug"></param>
        public void Debug(string message, string logs_path)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Debug";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, logs_path, type_log);
            }
        }
        /// <summary>
        /// Перегрузка метода  Error с двумя атрибутами
        /// </summary>
        /// <param name="Error"></param>
        public void Error(Type_Input_Loggers type_Input_Loggers, string message)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Debug || type_Input_Loggers == Type_Input_Loggers.Info ||  type_Input_Loggers == Type_Input_Loggers.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Error";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, type_log);
            }
        }
        /// <summary>
        /// Перегрузка метода  Error с одним атрибутом
        /// </summary>
        /// <param name="Error"></param>
        public void Error(string message, string logs_path)
        {
            if (type_Input_Loggers == Type_Input_Loggers.Debug || type_Input_Loggers == Type_Input_Loggers.Info ||  type_Input_Loggers == Type_Input_Loggers.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string type_log = string.Empty;
                if (output_console == true)
                {
                    type_log = "Error";
                    Console.Write($"{type_log}|");
                }
                Console.ResetColor(); // сбрасываем в стандартный
                Print(output_console, output_file,  message, logs_path, type_log);
            }
        }
    }

    /// <summary>
    /// тестирование работы
    /// </summary>

}

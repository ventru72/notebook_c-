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
            Log_Input log_input = new Log_Input();
            log_input.Output_console(true);
            log_input.Output_file(true);
            log_input.Type_Loggers(Type_Input_Loggers.Debug);
            //void Output_Test_String()
            //{
            //    string message = "Тестовое сообщение";
                 
            //    log_input.Error(message);
            //    log_input.Info(message);
            //    log_input.Debug(message);
            //    Console.ReadKey();
            //}
            //Output_Test_String();
            //string message = "Тестовое сообщение";
            //log_input.Debug(message);
            //Console.ReadKey();
            deserialization.Zip(deserialization.Desser_Json());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapicnaya_book
{
    internal class Program
    {
        enum Book 
        {
            Create = 1,
            Editing,
            Import,
            Sort,
            Delete,
            

        }
       static void Сhoice()
        {
            save s = new save();
            Console.WriteLine($"Выберите действие:");
            Console.WriteLine($"1 - Создать новый файл.\n2 - Загрузить файл из компьютера.");
            ConsoleKey key_w = new ConsoleKey();
            key_w = Console.ReadKey().Key;
            int number_new_index = 1;
            if (key_w == ConsoleKey.D1) s.Save_field( ref number_new_index);
            else if (key_w == ConsoleKey.D2) s.Load_field();  s.Add_next(); 
        }
         

        static void Main(string[] args)
        {
            Book book;
            save s = new save();
            List_book list_Book = new List_book();
            var values = Enum.GetValues(typeof(Book));
            foreach (var value in values)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine($"\nВыберите действие:");
            //if (Enum.IsDefined(typeof(Book), str))
            //{
            //    book = (Book)str;
            //}
            //else throw new Exception("Неверный запрос!");
            string str = Console.ReadLine();
            book = (Book)Enum.Parse(typeof(Book), str, ignoreCase: true);
            switch (book)
            {
                case Book.Create:
                    Сhoice();
                    break;
                case Book.Editing:
                    s.Editing();
                    break;
                case Book.Import:
                    s.Import();
                    break;
                case Book.Sort:
                   s.Sort();
                    break;
                case Book.Delete:
                    s.Delete();
                    break;
                default:
                    break;
            }

            Console.ReadLine();
            
        }
    }
}

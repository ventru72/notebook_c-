using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zapicnaya_book
{
  /// <summary>
  /// Заполнение массива и запись в файл.
  /// </summary>
    internal struct save
    {
        public int count1;
        //public int number_index=1;
        /// <summary>
        ///  метод - Заполнение массива и запись в файл.
        /// </summary>
        public void Save_field( ref int number_new_index)

        {
            Console.Write(@"Введите адрес и название файла в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            
            using (StreamWriter sw = new StreamWriter(adres_field, true, Encoding.UTF8))
            {
                ConsoleKey key_w = new ConsoleKey(); //выход из цикла вайл добавления записей

                //Array_book array_Book = new Array_book();//реализация через массив
                //array_Book.arr = new add_zapic[10];

                List_book list_Book = new List_book();
                list_Book.list = new List<add_zapic>();
                int number = number_new_index;
                int debt = 0;
                
                string name = string.Empty;
                string last_name = string.Empty;
                while (key_w != ConsoleKey.Enter)
                { 
                    Console.Write("Введите имя должника: ");
                    name = Console.ReadLine();
                    Console.Write("\nВведите Фамилию должника: ");
                    last_name = Console.ReadLine();
                    Console.Write("\nВведите сумму долга должника: ");
                    debt = int.Parse(Console.ReadLine());
                    string dateTime = DateTime.Now.ToString();
                    list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                    sw.Write($"{list_Book.list[count1].Number} ");
                    sw.Write($"{list_Book.list[count1].Name} ");
                    sw.Write($"{list_Book.list[count1].Last_name} ");
                    sw.Write($"{list_Book.list[count1].Debt} ");
                    sw.WriteLine($"{list_Book.list[count1].DateTime} ");
                    //array_Book.arr[count] = new add_zapic(_name, _Lname); реализация добавления записи через массив
                    //sw.Write($"{array_Book.arr[count].Get_name(_name)} ");
                    //sw.WriteLine($"{array_Book.arr[count].Get_last_name(_Lname)} ");
                    Console.WriteLine($"\nЗапись добавленна!");
                    Console.WriteLine($"Нажмите (Space) что бы продолжить ввод. Нажмите (Enter) если хотите остановить ввод записей! ");
                    key_w = Console.ReadKey().Key;
                    number++;
                     count1++;
                }
                sw.Close();
            }
        }
        /// <summary>
        ///  метод - Загрузка из файла и заполнение  массива структур.
        /// </summary>
        public void Load_field()
        {

            Console.Write(@"Введите адрес и название файла в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            List_book list_Book = new List_book();
            list_Book.list = new List<add_zapic>();

            int number = 1;
            int number_new_index = 1;
            int debt = 0;
            string dateTime = string.Empty;
            string name = string.Empty;
            string last_name = string.Empty;
            using (StreamReader sr = new StreamReader(adres_field, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr_st_line = line.Split(' ');
                        number = int.Parse( arr_st_line[0]);
                        name = arr_st_line[1];
                        last_name = arr_st_line[2];
                        debt = int.Parse(arr_st_line[3]);
                        dateTime = arr_st_line[4];
                        list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                        number_new_index++;
                }
                sr.Close();
            }
            Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  " );
             
            foreach (add_zapic item in list_Book.list)
            {
                Console.WriteLine(item.Print());
            }

            Console.WriteLine($"Нажмите (Space) что бы добавить запись. Нажмите (Enter) если хотите выйти! ");
            ConsoleKey key_w = new ConsoleKey();
            key_w = Console.ReadKey().Key;
            if (key_w != ConsoleKey.Enter)
            {
                Save_field(ref  number_new_index);
            }
        }
        /// <summary>
        /// Добавление записи - для метода  Load_field()
        /// </summary>
        public void Add_next()
        {
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        public void Delete()
        {
            Console.Write(@"Введите адрес и название файла в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            List_book list_Book = new List_book();
            list_Book.list = new List<add_zapic>();
            int number = 1;
            int debt = 0;
            string dateTime = string.Empty;
            string name = string.Empty;
            string last_name = string.Empty;
            using (StreamReader sr = new StreamReader(adres_field, Encoding.UTF8))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr_st_line = line.Split(' ');
                    number = int.Parse(arr_st_line[0]);
                    name = arr_st_line[1];
                    last_name = arr_st_line[2];
                    debt = int.Parse(arr_st_line[3]);
                    dateTime =  arr_st_line[4];
                    list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                }
                sr.Close();

            }
            Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  ");
            ConsoleKey key_w = new ConsoleKey();
            using (StreamWriter sw = new StreamWriter(adres_field, true, Encoding.UTF8))
            {
                while (key_w != ConsoleKey.Enter)
            {
                    foreach (add_zapic item in list_Book.list)
                    {
                        Console.WriteLine(item.Print());
                    }
                    Console.WriteLine($"Выбирите запись которую нужно удалить.");
                string number_str = Console.ReadLine();
                int number_l = int.Parse(number_str);
                list_Book.list.RemoveAt(number_l-1);
                    for (int i = 0; i < list_Book.list.Count; i++)
                    {   if (list_Book.list[i].Number > number_l)
                        {
                            int number1 = list_Book.list[i].Number - 1;
                            sw.Write($"{list_Book.list[i].Get_number(number1)} ");
                        }
                        else
                        {
                            int number1 = list_Book.list[i].Number;
                            sw.Write($"{list_Book.list[i].Get_number(number1)} ");
                        }
                        string name1 = list_Book.list[i].Name;
                        string last_name1 = list_Book.list[i].Last_name;
                        int debt1 = list_Book.list[i].Debt;
                        string dateTime1 = list_Book.list[i].DateTime;
                        sw.Write($"{list_Book.list[count1].Get_name(name1)} ");
                        sw.Write($"{list_Book.list[count1].Get_last_name(last_name1)} ");
                        sw.Write($"{list_Book.list[count1].Get_debt(debt1)} ");
                        sw.WriteLine($"{list_Book.list[count1].Get_data_time(dateTime1)} ");
                    }
                    Console.WriteLine($"Нажмите (Space) если хотите удалить еще запись. Нажмите (Enter) для выхода! ");
                key_w = Console.ReadKey().Key;
            }
                sw.Close();
            }
        }
        /// <summary>
        /// Редоктирование записи
        /// </summary>
        public void Editing()
        {
            Console.Write(@"Введите адрес и название файла который необходимо отредактировать, в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            List_book list_Book = new List_book();
            list_Book.list = new List<add_zapic>();
            int number = 1;
            int debt = 0;
            string dateTime = string.Empty;
            string name = string.Empty;
            string last_name = string.Empty;
            using (StreamReader sr = new StreamReader(adres_field, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr_st_line = line.Split(' ');
                    number = int.Parse(arr_st_line[0]);
                    name = arr_st_line[1];
                    last_name = arr_st_line[2];
                    debt = int.Parse(arr_st_line[3]);
                    dateTime = arr_st_line[4] ;
                    list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                }
                sr.Close();
            }
            Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  ");
            foreach (add_zapic item in list_Book.list)
            {
                Console.WriteLine(item.Print());
            }
            ConsoleKey key_w = new ConsoleKey();
            while (key_w != ConsoleKey.Enter)
            {
                using (StreamWriter sw = new StreamWriter(adres_field, true, Encoding.UTF8))
            {
                    Console.WriteLine($"Выбирите запись которую нужно отредактировать.");

                    string number_str = Console.ReadLine();
                     number = int.Parse(number_str);
                    list_Book.list.RemoveAt(number - 1);
                    Console.Write("Введите имя должника: ");
                    name = Console.ReadLine();
                    Console.Write("\nВведите Фамилию должника: ");
                    last_name = Console.ReadLine();
                    Console.Write("\nВведите сумму долга должника: ");
                    debt = int.Parse(Console.ReadLine());
                    Console.Write("\nВведите дату оформления долга в формате 13.05.2022 0:58:28 : ");
                    dateTime =  Console.ReadLine();
                    // number = number_l;
                    list_Book.list.Insert(number - 1, new add_zapic(number, name, last_name, debt, dateTime));
                   for(int i = 0; i < list_Book.list.Count; i++)
                    {
                         number = list_Book.list[i].Number;
                         name = list_Book.list[i].Name;
                         last_name = list_Book.list[i].Last_name;
                         debt = list_Book.list[i].Debt;
                         dateTime = list_Book.list[i].DateTime;

                        sw.Write($"{list_Book.list[count1].Get_number(number)} ");
                        sw.Write($"{list_Book.list[count1].Get_name(name)} ");
                        sw.Write($"{list_Book.list[count1].Get_last_name(last_name)} ");
                        sw.Write($"{list_Book.list[count1].Get_debt(debt)} ");
                        sw.WriteLine($"{list_Book.list[count1].Get_data_time(dateTime)} ");
                    }
                    sw.Close();
                    Console.WriteLine($"Нажмите (Space) если хотите отредактировать еще запись. Нажмите (Enter) для выхода! ");
                    key_w = Console.ReadKey().Key;
                }
            }
        }
        /// <summary>
        /// импорт записей
        /// </summary>
        public void Import()
        {
            Console.Write(@"Введите адрес и название исходного файла в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            List_book list_Book = new List_book();
            list_Book.list = new List<add_zapic>();
            int number = 1;
            int debt = 0;
            string dateTime = string.Empty;
            string name = string.Empty;
            string last_name = string.Empty;
            using (StreamReader sr = new StreamReader(adres_field, Encoding.UTF8))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr_st_line = line.Split(' ');
                    number = int.Parse(arr_st_line[0]);
                    name = arr_st_line[1];
                    last_name = arr_st_line[2];
                    debt = int.Parse(arr_st_line[3]);
                    dateTime =  arr_st_line[4];
                    list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                }
                sr.Close();

            }///////////// до этой черты шаблон считывания данных из файла
            Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  ");

            foreach (add_zapic item in list_Book.list)
            {
                Console.WriteLine(item.Print());
            }
            Console.Write(@"Введите адрес и название нового файла с импортируемыми записями, в формате (E:\dolg_import.txt): ");
            string adres_field_2 = Console.ReadLine();
            string adres_field_import = $@"{adres_field_2}";
            using (StreamWriter sw = new StreamWriter(adres_field_import, true, Encoding.UTF8))
            {
                    Console.WriteLine($"Введите диапазон записей который нужно импортировать (Число 1 - начало. Число 2 - конец).");
                    Console.Write("Число 1: ");
                    int number_1 = int.Parse(Console.ReadLine());
                    Console.Write("\nЧисло 2: ");
                    int number_2 = int.Parse(Console.ReadLine());
                //List_book list_Book_import = new List_book(); как вариант но не заработало что то
                //list_Book_import.list = new List<add_zapic>();
                //list_Book_import.list = list_Book.list.GetRange(number_1-1, number_2);
                    number = number_1;
                    list_Book.list.Insert(number_1 - 1, new add_zapic(number, name, last_name, debt, dateTime));
                    for (int i = number_1; i < number_2+1; i++)
                    {
                    int number1 = list_Book.list[i].Number;
                    string name1 = list_Book.list[i].Name;
                    string last_name1 = list_Book.list[i].Last_name;
                    int debt1 = list_Book.list[i].Debt;
                     string dateTime1 = list_Book.list[i].DateTime;

                    sw.Write($"{list_Book.list[count1].Get_number(number1)} ");
                    sw.Write($"{list_Book.list[count1].Get_name(name1)} ");
                    sw.Write($"{list_Book.list[count1].Get_last_name(last_name1)} ");
                    sw.Write($"{list_Book.list[count1].Get_debt(debt1)} ");
                    sw.WriteLine($"{list_Book.list[count1].Get_data_time(dateTime1)} ");
                }
                Console.WriteLine($"Нажмите (Enter) для завершения.");
                sw.Close();
            }
        }
        /// <summary>
        /// Сортировка.
        /// </summary>
        public void Sort()
        {
            Console.Write(@"Введите адрес и название  файла в формате (E:\dolg.txt): ");
            string adres_field_1 = Console.ReadLine();
            string adres_field = $@"{adres_field_1}";
            List_book list_Book = new List_book();
            list_Book.list = new List<add_zapic>();
            int number = 1;
            int debt = 0;
            string dateTime = string.Empty;
            string name = string.Empty;
            string last_name = string.Empty;
            using (StreamReader sr = new StreamReader(adres_field, Encoding.UTF8))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr_st_line = line.Split(' ');
                    number = int.Parse(arr_st_line[0]);
                    name = arr_st_line[1];
                    last_name = arr_st_line[2];
                    debt = int.Parse(arr_st_line[3]);
                    dateTime =  arr_st_line[4];
                    list_Book.list.Add(new add_zapic(number, name, last_name, debt, dateTime));
                }
                sr.Close();

            }
            Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  ");

            foreach (add_zapic item in list_Book.list)
            {
                Console.WriteLine(item.Print());
            }
            Console.WriteLine($"\nВыбирите тип сортировки и нажмите (Enter):\n1 - Cортировка по №\n2 - Cортировка по имени\n3 - Cортировка по фамилии" +
                $"\n4 - Сортировка по сумме долга\n5 - Сортировака по дате оформления долга");
            string str = Console.ReadLine();

            int ComparerForAge(add_zapic a, add_zapic b)
            {
                if (str == "2")
                {
                    int comp_Name = a.Name.CompareTo(b.Name);
                    if (comp_Name != 0)
                        return comp_Name;
                }
                else if (str == "3")
                {
                    int comp_Last_name = a.Name.CompareTo(b.Last_name);
                    if (comp_Last_name != 0)
                        return comp_Last_name;
                }
                else if (str == "4")
                {//если поменять местами "а" и "b" то изменится порядок сортировки
                    int comp_Debt = a.Debt.CompareTo(b.Debt); //наобарот
                    if (comp_Debt != 0)
                        return comp_Debt;
                }
                else if (str == "5")
                {
                    int comp_Data_Time = a.DateTime.CompareTo(b.DateTime);
                    if (comp_Data_Time != 0)
                        return comp_Data_Time;
                }
                return a.Number.CompareTo(b.Number);
        }
            using (StreamWriter sw = new StreamWriter(adres_field, true, Encoding.UTF8))
                {
                list_Book.list.Sort(ComparerForAge);

                    for (int i = 0; i < list_Book.list.Count; i++)
                    {

                     number = list_Book.list[i].Number;
                     name = list_Book.list[i].Name;
                     last_name = list_Book.list[i].Last_name;
                     debt = list_Book.list[i].Debt;
                     dateTime = list_Book.list[i].DateTime;

                    sw.Write($"{list_Book.list[count1].Get_number(number)} ");
                    sw.Write($"{list_Book.list[count1].Get_name(name)} ");
                    sw.Write($"{list_Book.list[count1].Get_last_name(last_name)} ");
                    sw.Write($"{list_Book.list[count1].Get_debt(debt)} ");
                    sw.WriteLine($"{list_Book.list[count1].Get_data_time(dateTime)} ");
                }
                    sw.Close();
                Console.WriteLine($"№  Имя    Фамилия  Долг  Дата оформления долга  ");

                foreach (add_zapic item in list_Book.list)
                {
                    Console.WriteLine(item.Print());
                }
                Console.WriteLine($"Нажмите (Enter) что бы сохранить отсортированный список и выйти ");
                    
            }
        }

    }
    
}
//int comp_Name = a.Name.CompareTo(b.Name);
//if (comp_Name != 0)
//    return comp_Name;
//int comp_Last_name = a.Name.CompareTo(b.Last_name);
//if (comp_Last_name != 0)
//    return comp_Last_name;

//return a.Number.CompareTo(b.Number);




//switch (str)
//{
//    case "2":
//        int comp_Name = a.Name.CompareTo(b.Name);
//        if (comp_Name != 0)
//            return comp_Name;
//        break;
//    case "3":
//        int comp_Last_name = a.Name.CompareTo(b.Last_name);
//        if (comp_Last_name != 0)
//            return comp_Last_name;
//        break;

//    default:
//        return a.Number.CompareTo(b.Number);
//        break;
using EBookLib;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace PeerGrade
{
    internal class Program
    {   
        /// <summary>
        /// Генерирует рандомную строку заданной длины.
        /// </summary>
        /// <param name="length">Длина строки.</param>
        /// <returns>Рандомно сгенерированная строка заданной длины.</returns>
        public static string GetRandomString(int length)
        {
            Random random = new Random();
            string result = "";
            for(int i = 0; i < length; i++)
            {
                if (i == 0) result+=(char)random.Next('A', 'Z' + 1);
                else result+=(char)random.Next('a', 'z' + 1);
            }

            return result;
        }
        /// <summary>
        /// Заполняет библиотеку случайными печатными изданиями (книгами и журналами) в заданном количестве.
        /// </summary>
        /// <param name="n">Количество печатных изданий</param>
        /// <param name="library">Библиотека</param>
        public static void FillLibrary(int n, ref MyLibrary<PrintEdition> library)
        {
            Random random = new Random();
            for(int i = 0; i < n; i++)
            {
                do
                {
                    try
                    {
                        if (random.Next(2) == 0)
                        {
                            string random_name = GetRandomString(random.Next(1, 11));
                            int random_pages = random.Next(-10, 101);
                            int random_period = random.Next(-10, 101);
                            library.Add(new Magazine(random_name, random_pages, random_period));
                            library[i].onPrint += PrintHandler;
                        }
                        else
                        {
                            string random_name = GetRandomString(random.Next(1, 11));
                            int random_pages = random.Next(-10, 101);
                            string random_author = GetRandomString(random.Next(1, 11));
                            library.Add(new Book(random_name, random_pages, random_author));
                            library[i].onPrint += PrintHandler;
                        }
                        break;
                    }
                    catch(ArgumentException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                } while (true);               
            }           
        }
        /// <summary>
        /// Обработчик события onPrint: выводит специальное сообщение и информацию о печатном издании.
        /// </summary>
        /// <param name="sender">Подписчик события.</param>
        /// <param name="e">Аргументы.</param>
        public static void PrintHandler(object? sender, EventArgs e)
        {
            Console.WriteLine("PRINTED!");
            Console.WriteLine($"Информация о печатном издании: {(PrintEdition)sender}");
        }
        /// <summary>
        /// Обработчик события onTake: выводит специальное сообщение.
        /// </summary>
        /// <param name="sender">Подписчик события.</param>
        /// <param name="e">Аргументы.</param>
        public static void TakeHandler(object? sender, TakeEventArgs e)
        {
            Console.WriteLine($"Attention! Books starts with {e.StartSymbol} were taken");
        }
        /// <summary>
        /// Выводит результат выполнения метода Print для всех книг в библиотеке.
        /// </summary>
        /// <param name="library">Библиотека.</param>
        public static void PrintBooks(MyLibrary<PrintEdition> library)
        {
            bool flag = true;
            foreach (var el in library)
            {
                if (el.GetType() == typeof(Book))
                {
                    el.Print();
                    flag = false;
                }
            }              
            if(flag)
                Console.WriteLine("В библиотеке нет книг");
        }
        /// <summary>
        /// Выводит информацию о каждом печатном издании в библиотеке.
        /// </summary>
        /// <param name="library">Библиотека.</param>
        public static void PrintLibrary(MyLibrary<PrintEdition> library)
        {
            bool flag = true;
            int i = 1;
            foreach (var el in library)
            {
                Console.WriteLine($"{i++}. {el}");
                flag = false;
            }
            if(flag)
                Console.WriteLine("В библиотеке нет печатных изданий");
        }
        /// <summary>
        /// Выполняет сериализацию в JSON и записывает результат в текстовый файл, расположенный в одной папке с исполняемым файлом проекта.
        /// </summary>
        /// <param name="library">Библиотека.</param>
        public static void JsonSerializeLibrary(MyLibrary<PrintEdition> library)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MyLibrary<PrintEdition>));
            FileStream fs = new FileStream("myLibrary.txt", FileMode.Create);
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, System.Text.Encoding.Unicode, false, true))
            {
                serializer.WriteObject(writer, library);
                Console.WriteLine("Сериализация в JSON успешно завершена!");
            }
            fs.Flush();
            fs.Close();
        }
        /// <summary>
        /// Выполняет десериализацию из JSON и записывает результат в передаваемую библиотеку.
        /// <param name="library">Библиотека.</param>
        public static void JsonDeserializeLibrary(out MyLibrary<PrintEdition> library)
        {
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(MyLibrary<PrintEdition>));
            using (var fs = new FileStream("myLibrary.txt", FileMode.Open))
            {
                library = deserializer.ReadObject(fs) as MyLibrary<PrintEdition>;
                Console.WriteLine("Десериализация из JSON успешно завершена!");
            }
        }
        /// <summary>
        /// Выводит информацию о среднем числе страниц во всех книгах и журналах библиотеки.
        /// </summary>
        /// <param name="library">Библиотека.</param>
        public static void PrintAverageBookAndMagazinePages(MyLibrary<PrintEdition> library)
        {
            if(library.GetAverageBookPages == -1)
                Console.WriteLine("0 (в библиотеке нет книг)");
            else
                Console.WriteLine($"Среднее число страниц во всех книгах: {library.GetAverageBookPages:f2}");
            if (library.GetAverageMagazinePages == -1)
                Console.WriteLine("0 (в библиотеке нет журналов)");
            else
                Console.WriteLine($"Среднее число страниц во всех журналах: {library.GetAverageMagazinePages:f2}");           
        }
        static void Main(string[] args)
        {
            int n;
            char startSymbol;
            bool flag = false;
            ConsoleKeyInfo key;
            Random random = new Random();
            do
            {
                try
                {
                    MyLibrary<PrintEdition> myLibrary = new MyLibrary<PrintEdition>();
                    Console.Clear();
                    Console.Write("Введите целое значение n - количество печатных изданий: ");
                    while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                    {
                        Console.Clear();
                        Console.Write("Вы ввели или не целое значение n, или отрицательное значчение n, или n не является числом, повторите ввод: ");
                    }
                    FillLibrary(n, ref myLibrary);
                    Console.WriteLine("---------------------------------------------------------");
                    startSymbol = (char)random.Next('A', 'Z' + 1);
                    myLibrary.onTake += TakeHandler;
                    Console.WriteLine("Печатные издания в библиотеке до выемки книг: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintLibrary(myLibrary);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Результат вызова метода Print для всех книг библиотеки: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintBooks(myLibrary);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("---------------------------------------------------------");
                    myLibrary.TakeBooks(startSymbol);
                    Console.WriteLine("Печатные издания в библиотеке после выемки книг: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintLibrary(myLibrary);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("---------------------------------------------------------");
                    JsonSerializeLibrary(myLibrary);
                    Console.WriteLine("---------------------------------------------------------");
                    MyLibrary<PrintEdition> newMyLibrary;
                    JsonDeserializeLibrary(out newMyLibrary);
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Восстановленная библиотека: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintLibrary(newMyLibrary);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("---------------------------------------------------------");
                    PrintAverageBookAndMagazinePages(newMyLibrary);
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Для повторного ввода нажмите R\nДля выхода выхода нажмите Q");
                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Q)
                        {
                            flag = true;
                            break;
                        }
                        if (key.Key == ConsoleKey.R)
                        {
                            flag = false;
                            break;
                        }
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: некорректная клавиша, повторите ввод: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Для повторного ввода нажмите R\nДля выхода выхода нажмите Q");
                    } while (true);
                    if (flag)
                        break;
                    else
                        continue;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Что-то пошло не так");
                    Console.WriteLine($"Сообщение об ошибке: {ex.Message}");
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Нажмите Q для выхода и любую другую клавишу для продолжения");
                    if (Console.ReadKey(true).Key == ConsoleKey.Q)
                        break;                  
                }                  
            } while (true);

            Console.Clear();
            Console.WriteLine("Программа завершена!");
        }
    }
}
using System.Runtime.Serialization;
using System.Text;

namespace EKRLib
{
    /// <summary>
    /// Represents the method of getting random line that consists of 5 elements, the methods that write down all information about cars or motorboats in special files and the method of getting random list in range of 6 to 9 elements and the method that represents the menu
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Checking if the model consists only of capital latin letters or digits and has only 5 symbols
        /// </summary>
        /// <param name="model">Input model that have to be checked</param>
        /// <returns>True if model is good false otherwise</returns>
        public static bool CheckModel(string model)
        {
            for (int i = 0; i <model.Length; i++)
            {
                if (!"QWERTYUIOPASDFGHJKLZXCVBNM1234567890".Contains(model[i])) return false;
            }

            if (model.Length == 5) return true;
            else return false;
        }
        /// <summary>
        /// Returns the random five-element line that consists of capital latin letters or digits
        /// </summary>
        /// <returns>Random five-element line that consists of capital latin letters or digits</returns>
        public static string GetRandomLine()
        {
            Random rnd = new Random();
            string line = "";

            for (int i = 0; i < 5; i++)
            {
                if (rnd.Next(2) == 0)
                    line+=(char)rnd.Next(48, 58);
                else
                    line+=(char)rnd.Next(65, 91);
            }

            return line;
        }
        /// <summary>
        /// Creates the file that consists of lines about cars from input list
        /// </summary>
        /// <param name="list">Input list that contains the lines about cars and motorboats</param>
        public static void WriteCarsInFile(List<Transport> list)
        {
            using (StreamWriter sw = new StreamWriter("../../../../Cars.txt", false, Encoding.Unicode))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].GetType() == typeof(Car)) sw.WriteLine(list[i]);
                }
            }
        }
        /// <summary>
        /// Creates the file that consists of lines about motorboats from input list
        /// </summary>
        /// <param name="list">Input list that contains the information about cars and motorboats</param>
        public static void WriteMotorBoatsInFile(List<Transport> list)
        {
            using (StreamWriter sw = new StreamWriter("../../../../MotorBoats.txt", false, Encoding.Unicode))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].GetType() == typeof(MotorBoat)) sw.WriteLine(list[i]);
                }
            }
        }
        /// <summary>
        /// Fills the list with 6 to 9 elements by random cars or motorboats and print it
        /// </summary>
        /// <param name="list">The list that is filled in by the method</param>
        public static void GetList(ref List<Transport> list)
        {
            Random rnd = new Random();
            int count = rnd.Next(6, 10), k;
            Console.WriteLine("Your random list: ");
            Console.WriteLine();
            for (int i = 0; i < count; i++)
            {
                k = 0;
                do
                {
                    try
                    {
                        if (rnd.Next(2) == 0)
                        {
                            list.Add(new Car(GetRandomLine(), (uint)rnd.Next(10, 100)));
                        }
                        else
                        {
                            list.Add(new MotorBoat(GetRandomLine(), (uint)rnd.Next(10, 100)));
                        }

                        Console.WriteLine(list[i].StartEngine());
                        k++;
                    }
                    catch (TransportException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (k!=1);
            }
        }
        /// <summary>
        /// Represents the commands to interact with program
        /// </summary>
        /// <exception cref="KeyNotFoundException">The exception that is thrown when the entered key is not valid</exception>
        public static void Menu()
        {
            string[] commands = new string[]
            {
                "Press 0 to exit the program",
                "Press 1 to build a new random list of transports (cars or motorboats)",
                "Press 1 if you want to write down all information about cars in file \"Cars.txt\"",
                "Press 2 if you want to write down all information about motorboats in file \"MototBoats.txt\"",
                "Press 3 if you want to write down all information about cars and motorboats in files \"Cars.txt\" and \"MototBoats.txt\" respectively",
                "Press 4 if you do not want to write down any information about cars or motorboats in files",
            };

            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"{commands[0]}\n{commands[1]}");
                    ConsoleKey key1 = Console.ReadKey(true).Key;
                    if (key1 == ConsoleKey.D0) break;
                    else if (key1 == ConsoleKey.D1)
                    {
                        Console.Clear();
                        List<Transport> list = new List<Transport>();
                        GetList(ref list);
                        Console.WriteLine();

                        do
                        {
                            try
                            {
                                for (int i = 2; i < commands.Length; i++)
                                {
                                    Console.WriteLine(commands[i]);
                                }
                                ConsoleKey key2 = Console.ReadKey(true).Key;
                                if (key2 == ConsoleKey.D1)
                                {
                                    WriteCarsInFile(list);
                                    Console.WriteLine();
                                    Console.WriteLine("Successfully! Press any key to continue");
                                    Console.ReadKey(true);
                                    break;
                                }
                                if (key2 == ConsoleKey.D2)
                                {
                                    WriteMotorBoatsInFile(list);
                                    Console.WriteLine();
                                    Console.WriteLine("Successfully! Press any key to continue");
                                    Console.ReadKey(true);
                                    break;
                                }
                                if (key2 == ConsoleKey.D3)
                                {
                                    WriteCarsInFile(list);
                                    WriteMotorBoatsInFile(list);
                                    Console.WriteLine();
                                    Console.WriteLine("Successfully! Press any key to continue");
                                    Console.ReadKey(true);
                                    break;
                                }
                                if (key2 == ConsoleKey.D4)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Successfully! Press any key to continue");
                                    Console.ReadKey(true);
                                    break;
                                }
                                throw new KeyNotFoundException();
                            }
                            catch (Exception ex)
                            {
                                if (ex is KeyNotFoundException) Console.Write("Wrong key. Press any key to try again");
                                else Console.WriteLine("Something went wrong. Press any key to try again");
                                Console.ReadKey(true);
                                Console.Clear();
                                continue;
                            }
                        } while (true);
                    }
                    else throw new KeyNotFoundException();
                }
                catch (Exception ex)
                {
                    if (ex is KeyNotFoundException) Console.Write("Wrong key. Press any key to try again");
                    else Console.WriteLine("Something went wrong. Press any key to try again");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            } while (true);
            Console.Clear();
            Console.WriteLine("Program was finished");
        }
    }
}
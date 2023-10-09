using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DifferentDevices
{
    internal class Menu
    {
        /// <summary>
        /// Represents a menu to the user
        /// </summary>
        public Menu()
        {
            Console.WriteLine("MENU:\nPress \"0\" to exit\nPress \"1\" to input file name to build necessary files");
        }
        /// <summary>
        /// Represents commands to manage the application and outputs corresponding messages after executing commands
        /// </summary>
        /// <returns>True if command is to finish; otherwise, false</returns>
        public bool IsFinished()
        {
            int n;
            string? command;
            Console.WriteLine();
            Console.Write("Input your command, please: ");
            command = Console.ReadLine();
            Console.Clear();
            if (int.TryParse(command, out n) && (n == 0 || n == 1))
            {
                if (n == 0)
                {
                    Console.WriteLine("Program was finished");
                    return true;
                }
                if (n == 1)
                {
                    Logic logic = new Logic();
                    logic.Files();
                    Console.WriteLine("MENU:\nPress \"0\" to exit\nPress \"1\" to input file name to build necessary files");
                    return false;
                }
                return false;
            }
            else
            {
                Console.WriteLine("Wrong command. Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
                Console.WriteLine("MENU:\nPress \"0\" to exit\nPress \"1\" to input file name to build necessary files");
                return false;
            }
        }
    }
}

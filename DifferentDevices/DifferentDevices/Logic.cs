using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DifferentDevices
{
    internal class Logic
    {
        string? file_name;
        /// <summary>
        /// Writes a special line and suggests the user to input file name 
        /// </summary>
        public Logic()
        {
            Console.Write("Input your file name to build necessary files, please: ");
            file_name = Console.ReadLine();
        }
        /// <summary>
        /// Buildes necessary files and represents special message to user if it is not possible
        /// </summary>
        public void Files()
        {          
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("windows-1251");
                List<string> mas = new List<string>();
                List<PC> computers = new List<PC>();
                List<Phone> phones = new List<Phone>();
                string? s;
                string[] line;

                using (StreamReader sr = new StreamReader(file_name, encoding))
                {
                    s = sr.ReadLine();
                    while(s != null)
                    {
                        if(s != string.Empty) mas.Add(s);
                        s = sr.ReadLine();
                    }
                }

                if (mas.Count == 0) throw new ParseException("Empty file");
                if(!int.TryParse(mas[mas.Count - 1].Split(' ')[2], out int M) || !int.TryParse(mas[mas.Count - 2].Split(' ')[2], out int N) || N<=0 || M<=0)
                    throw new ParseException();
                for (int i = 0; i < mas.Count - 3; i++)
                {
                    line = mas[i].Split(' ');
                    if (line[0] == "PC")
                    {
                        if (line.Length != 5) throw new ParseException();
                        if (!int.TryParse(line[1], out int k1) || !int.TryParse(line[3], out int k3) ||
                            !int.TryParse(line[4], out int k4)) throw new ParseException("Wrong format of parameters");
                        PC pc = new PC(k1, line[2], k3, k4);
                        if (pc.GraphicsCardsCount == N) computers.Add(pc);
                        continue;
                    }
                    if (line[0] == "Phone")
                    {
                        if (line.Length != 4) throw new ParseException();
                        if (!int.TryParse(line[1], out int k1) || !double.TryParse(line[3].Replace('.', ','), out double k3))
                            throw new ParseException("Wrong format of parameters");
                        Phone phone = new Phone(k1, line[2], k3);
                        if (phone.ScreenDiagonal > M) phones.Add(phone);
                        continue;
                    }
                    throw new ParseException();
                }

                if(computers.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter($"{file_name[..^4]} consisted of computers.txt", false, encoding))
                    {
                        for (int i = 0; i < computers.Count; i++)
                        {
                            sw.WriteLine(computers[i]);
                        }
                    }
                }
               
                if(phones.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter($"{file_name[..^4]} consisted of phones.txt", false, encoding))
                    {
                        for (int i = 0; i < phones.Count; i++)
                        {
                            sw.WriteLine(phones[i]);
                        }
                    }
                }                

                Console.WriteLine("Have done! Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}

using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Catchers_of_UFO
{
    /// <summary>
    /// Represents error that occur when file has wrong structure
    /// </summary>
    class StructureException : Exception
    {
        public StructureException() : base("File has wrong structure")
        {
        }

        public StructureException(string? message) : base(message)
        {
        }

        public StructureException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StructureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents error that occur when file does not have UTF8 encoding
    /// </summary>
    class EncodingException : Exception
    {
        public EncodingException() : base("File has wrong encoding")
        {
        }

        public EncodingException(string? message) : base(message)
        {
        }

        public EncodingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EncodingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents methods for analysing csv-files that contains records of UFO observations
    /// </summary>
    internal class UFO
    {
        string[] s, mas;
        List<double> latitude = new List<double>();
        List<string> data = new List<string>();
        List<string> Date_time = new List<string>();
        List<string> shape = new List<string>();
        List<string> time_duration = new List<string>();

        /// <summary>
        /// Initializes a new instance of UFO class
        /// </summary>
        /// <param name="path">File address</param>
        public UFO(string path)
        {
            s = File.ReadAllLines(path);
            for(int i = 0; i < s.Length; i++)
            {
                data.Add(s[i]);
            }

            FileStream fs = new FileStream(path, FileMode.Open);
            using(StreamReader sr = new StreamReader(fs, true))
            {
                if (sr.CurrentEncoding != Encoding.UTF8) throw new EncodingException();
            }
            mas = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (mas.Length != 11) throw new StructureException();
            for (int i = 0; i < data.Count; i++)
            {
                mas = data[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
                Date_time.Add(mas[0]);
                shape.Add(mas[4]);
                time_duration.Add(mas[5]);
                if (mas.Length > 9 && i >= 1) latitude.Add(double.Parse(mas[9].Replace('.', ',')));               
            }
        }

        /// <summary>
        /// Displays on the screen information about all dates when triangle UFO was observed for more than 1000 seconds
        /// </summary>
        public void WriteShapeTime()
        {
            int p;

            for (int i = 0; i < data.Count; i++)
            {
                if (int.TryParse(time_duration[i], out p)) p = int.Parse(time_duration[i]);
                if (shape[i] == "triangle" && p > 1000) Console.WriteLine(s[i]);
            }
        }

        /// <summary>
        /// Saves all records about all dates when triangle UFO was observed for more than 1000 seconds into csv-file "UFO-Shape-Time.csv"
        /// </summary>
        public void SaveShapeTime()
        {
            int p;
            string path = "UFO-Shape-Time.csv";

            for (int i = 0; i < data.Count; i++)
            {
                if (int.TryParse(time_duration[i], out p)) p = int.Parse(time_duration[i]);
                if (shape[i] == "triangle" && p > 1000)
                {
                    File.AppendAllText(path, s[i]);
                    File.AppendAllText(path, "\n");
                }
            }
        }

        /// <summary>
        /// Displays on the screen information about all records in which cylinder, triangle or circle UFO was observed in the period between 20:00 and 06:30"
        /// </summary>
        public void WriteSchedule()
        {
            int hours, minutes;

            for (int i = 1; i < data.Count; i++)
            {
                bool shape_flag = false, hour_flag = false;
                hours = int.Parse(Date_time[i].Substring(Date_time[i].Length - 5, 2));
                minutes = int.Parse(Date_time[i].Substring(Date_time[i].Length - 2));
                string[] shapes = { "triangle", "cylinder", "circle" };
                int[] ints = { 20, 21, 22, 23, 0, 1, 2, 3, 4, 5 };

                foreach (string sh in shapes)
                {
                    if (sh == shape[i]) shape_flag = true;
                }

                foreach (int j in ints)
                {
                    if (hours == j) hour_flag = true;
                }

                if (shape_flag && (hour_flag || (hours == 6 && minutes >=0 && minutes <= 30)))
                    Console.WriteLine(s[i]);
            }
        }

        /// <summary>
        /// Saves all records in which cylinder, triangle or circle UFO was observed in the period between 20:00 and 06:30 into csv-file "UFOs-Schedule.csv"
        /// </summary>
        public void SaveSchedule()
        {
            string path = "UFOs-Schedule.csv";
            int hours, minutes;

            for (int i = 1; i < data.Count; i++)
            {
                bool shape_flag = false, hour_flag = false;
                hours = int.Parse(Date_time[i].Substring(Date_time[i].Length - 5, 2));
                minutes = int.Parse(Date_time[i].Substring(Date_time[i].Length - 2));
                string[] shapes = { "triangle", "cylinder", "circle" };
                int[] ints = { 20, 21, 22, 23, 0, 1, 2, 3, 4, 5 };

                foreach (string sh in shapes)
                {
                    if (sh == shape[i]) shape_flag = true;
                }

                foreach (int j in ints)
                {
                    if (hours == j) hour_flag = true;
                }

                if (shape_flag && (hour_flag || (hours == 6 && minutes >=0 && minutes <= 30)))
                {
                    File.AppendAllText(path, s[i]);
                    File.AppendAllText(path, "\n");
                }
            }
        }

        /// <summary>
        /// Displays on the screen source dateset about UFO that is grouped by city column and in each group records are arranged in increasing latitude
        /// </summary>
        public void WriteGrouped()
        {
            List<string> cities = new List<string>();
            List<string> new_data = new List<string>();
            StringBuilder sb = new StringBuilder();
            DateTime dt = new DateTime();
            string str, title = data[0];
            bool flag = true;
            int p = 0;
            data.RemoveAt(0);

            for (int i = 0; i < data.Count; i++)
            {
                sb = new StringBuilder(Date_time[i]);
                sb.Replace("24:00", "00:00");
                if (DateTime.TryParse(sb.ToString(), out dt) && dt.DayOfWeek == DayOfWeek.Tuesday)
                    data.RemoveAt(i);
            }

            for (int i = 0; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                str = mas[0];
                mas[0] = mas[1];
                mas[1] = str;
                data[i] = String.Join(',', mas);
            }

            data.Sort();
            for (int i = 0; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                cities.Add(mas[0]);
            }

            for (int i = 0; i < cities.Count - 1; i++)
            {
                if (flag)
                {
                    new_data.Add(data[i]);
                    flag = false;
                    p = i;
                }
                else
                {
                    if (cities[i] == cities[i+1]) new_data.Add(data[i+1]);
                    else
                    {
                        flag = true;
                        for (int j = 0; j < new_data.Count; j++)
                        {
                            string[] mas = new_data[j].Split(',');
                            if (mas.Length > 9)
                            {
                                str = mas[0];
                                mas[0] = mas[9];
                                mas[9] = str;
                                new_data[j] = String.Join(',', mas);
                            }
                        }
                        new_data.Sort();
                        for (int j = 0; j < new_data.Count; j++)
                        {
                            string[] mas = new_data[j].Split(',');
                            if (mas.Length > 9)
                            {
                                str = mas[0];
                                mas[0] = mas[9];
                                mas[9] = str;
                                new_data[j] = String.Join(',', mas);
                            }
                        }
                        data.RemoveRange(p, new_data.Count);
                        data.InsertRange(p, new_data);
                        new_data.Clear();
                    }
                }
            }

            for (int j = 0; j < data.Count; j++)
            {
                string[] mas = data[j].Split(',');
                str = mas[0];
                mas[0] = mas[1];
                mas[1] = str;
                data[j] = String.Join(',', mas);
                data[j].Replace(",,", ",");
                data[j].Replace(",,,", ",");
            }

            Console.WriteLine(title);
            foreach (var el in data) Console.WriteLine(el);
        }

        /// <summary>
        /// Saves source dateset about UFO that is grouped by city column and in each group records are arranged in increasing latitude into csv-file "Grouped-UFOs.csv"
        /// </summary>
        public void SaveGrouped()
        {
            List<string> cities = new List<string>();
            List<string> new_data = new List<string>();
            StringBuilder sb = new StringBuilder();
            DateTime dt = new DateTime();
            string str, title = data[0], path = "Grouped-UFOs.csv";
            bool flag = true;
            int p = 0;
            data.RemoveAt(0);

            for (int i = 0; i < data.Count; i++)
            {
                sb = new StringBuilder(Date_time[i]);
                sb.Replace("24:00", "00:00");
                if (DateTime.TryParse(sb.ToString(), out dt) && dt.DayOfWeek == DayOfWeek.Tuesday)
                    data.RemoveAt(i);
            }

            for (int i = 0; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                str = mas[0];
                mas[0] = mas[1];
                mas[1] = str;
                data[i] = String.Join(',', mas);
            }

            data.Sort();
            for (int i = 0; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                cities.Add(mas[0]);
            }

            for (int i = 0; i < cities.Count - 1; i++)
            {
                if (flag)
                {
                    new_data.Add(data[i]);
                    flag = false;
                    p = i;
                }
                else
                {
                    if (cities[i] == cities[i+1]) new_data.Add(data[i+1]);
                    else
                    {
                        flag = true;
                        for (int j = 0; j < new_data.Count; j++)
                        {
                            string[] mas = new_data[j].Split(',');
                            if (mas.Length > 9)
                            {
                                str = mas[0];
                                mas[0] = mas[9];
                                mas[9] = str;
                                new_data[j] = String.Join(',', mas);
                            }
                        }
                        new_data.Sort();
                        for (int j = 0; j < new_data.Count; j++)
                        {
                            string[] mas = new_data[j].Split(',');
                            if (mas.Length > 9)
                            {
                                str = mas[0];
                                mas[0] = mas[9];
                                mas[9] = str;
                                new_data[j] = String.Join(',', mas);
                            }
                        }
                        data.RemoveRange(p, new_data.Count);
                        data.InsertRange(p, new_data);
                        new_data.Clear();
                    }
                }
            }

            for (int j = 0; j < data.Count; j++)
            {
                string[] mas = data[j].Split(',');
                str = mas[0];
                mas[0] = mas[1];
                mas[1] = str;
                data[j] = String.Join(',', mas);
                data[j].Replace(",,", ",");
                data[j].Replace(",,,", ",");
            }

            File.AppendAllText(path, title);
            File.AppendAllText(path, "\n");
            for (int i = 0; i < data.Count; i++)
            {
                File.AppendAllText(path, data[i]);
                File.AppendAllText(path, "\n");
            }
        }
        /// <summary>
        /// Writes summary statistics about loaded file
        /// </summary>
        public void Statistics()
        {
            List<string> list = new List<string>();
            List<string> forms = new List<string>();
            StringBuilder sb = new StringBuilder();
            string str;
            bool flag = true;
            double p, sum = 0, count = 0, average_duration, median, k = 1, s = 0;
            int m = 0;

            for(int i = 0; i < shape.Count; i++)
            {
                if (shape[i] == "triangle")
                {
                    if (double.TryParse(time_duration[i], out p))
                    {
                        sum+=int.Parse(time_duration[i]);
                        count++;
                    }
                }
            }
            average_duration = sum/count;

            latitude.Sort();
            if(latitude.Count % 2 == 1) median = latitude[(latitude.Count - 1)/2];
            else median = (latitude[latitude.Count/2] + latitude[latitude.Count/2 - 1])/2;

            for (int i = 1; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                str = mas[0];
                mas[0] = mas[4];
                mas[4] = str;
                data[i] = String.Join(',', mas);
            }
            data.Sort();
            
            for (int i = 1; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                forms.Add(mas[0]);
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] == mas[0]) flag = false;
                }
                if (flag) list.Add(mas[0]);
                flag = true;
            }

            Console.Write("Total number of records: ");
            Console.WriteLine(data.Count - 1);
            Console.WriteLine();
            Console.WriteLine("Statictics of UFO shape: ");
            Console.WriteLine();
            for (int i = 0; i < forms.Count-1; i++)
            {
                if (forms[i] == forms[i+1]) k++;
                else
                {
                    Console.WriteLine($"{list[m]}: {(100*k)/forms.Count}%");
                    s+=(100*k)/forms.Count;
                    m++;
                    k = 1;
                }
            }
            Console.WriteLine($"{list[list.Count-1]}: {100-s}%");
            Console.WriteLine();
            Console.Write("Average time duration of triangle UFO observation: ");
            Console.WriteLine(average_duration);
            Console.WriteLine();
            Console.Write("Median of latitude: ");
            Console.WriteLine(median);
            list.Clear();
            forms.Clear();
            for (int i = 1; i < data.Count; i++)
            {
                string[] mas = data[i].Split(',');
                str = mas[0];
                mas[0] = mas[4];
                mas[4] = str;
                data[i] = String.Join(',', mas);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] commands =
            {
                "MENU:",
                "Press \"1\" to input new file address",
                "Press \"2\" to display on the screen information about all dates when triangle UFO was observed for more than 1000 seconds",
                "Press \"3\" to display on the screen source dateset about UFO that is grouped by city column and in each group records are arranged in increasing latitude",
                "Press \"4\" to display on the screen information about all records in which cylinder, triangle or circle UFO was observed in the period between 20:00 and 06:30",
                "Press \"5\" to display on the screen summary statistics of loaded file",
                "Press \"6\" to save information about all dates when triangle UFO was observed for more than 1000 seconds into csv-file \"UFO-Shape-Time.csv\"",
                "Press \"7\" to save source dateset about UFO that is grouped by city column and in each group records are arranged in increasing latitude into csv-file \"Grouped-UFOs.csv\"",
                "Press \"8\" to save information about all all records in which cylinder, triangle or circle UFO was observed in the period between 20:00 and 06:30 into csv-file \"UFOs-Schedule.csv\"",
                "Press \"0\" to exit",
            };

            int n, k = 0;
            string path, s;
            ConsoleKeyInfo button;
            
            do
            {
                try
                {
                    Console.Write("Please input your file address: ");
                    path = Console.ReadLine();

                    UFO ufo = new UFO(path);
                    Console.Clear();
                    Console.WriteLine($"Program is working with {path}");
                    Console.WriteLine();
                    foreach (var command in commands) Console.WriteLine(command);
                    Console.WriteLine();
                    do
                    {
                        Console.Write("Input your command, please: ");
                        s = Console.ReadLine();

                        if (!int.TryParse(s, out n) || (n != 0 && n != 1 && n != 2 && n != 3 && n != 4 && n != 5 && n != 6
                            && n != 7 && n != 8))
                        {
                            Console.WriteLine("Wrong command, try again, please. Press any button to get back to the menu");
                            button = Console.ReadKey(true);
                            Console.Clear();
                            Console.WriteLine($"Program is working with {path}");
                            Console.WriteLine();
                            foreach (var command in commands) Console.WriteLine(command);
                            Console.WriteLine();
                        }
                        else
                        {
                            if (n == 0) break;
                            if(n == 1)
                            {
                                k = 1;
                                break;
                            }
                            if (n == 2)
                            {
                                Console.Clear();
                                ufo.WriteShapeTime();
                                Console.WriteLine();
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 3)
                            {
                                Console.Clear();
                                ufo.WriteGrouped();
                                Console.WriteLine();
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 4)
                            {
                                Console.Clear();
                                ufo.WriteSchedule();
                                Console.WriteLine();
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 5)
                            {
                                Console.Clear();
                                ufo.Statistics();
                                Console.WriteLine();
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 6)
                            {
                                ufo.SaveShapeTime();
                                Console.WriteLine();
                                Console.WriteLine("File \"UFO-Shape-Time.csv\" was saved");
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 7)
                            {
                                ufo.SaveGrouped();
                                Console.WriteLine();
                                Console.WriteLine("File \"Grouped-UFOs.csv\" was saved");
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                            if (n == 8)
                            {
                                ufo.SaveSchedule();
                                Console.WriteLine();
                                Console.WriteLine("File \"UFOs-Schedule.csv\" was saved");
                                Console.WriteLine("Press any button to get back to the menu");
                                button = Console.ReadKey(true);
                                Console.Clear();
                                Console.WriteLine($"Program is working with {path}");
                                Console.WriteLine();
                                foreach (var command in commands) Console.WriteLine(command);
                                Console.WriteLine();
                            }
                        }
                        
                    } while (true);
                    if(k == 1)
                    {
                        Console.Clear();
                        k = 0;
                        continue;
                    }
                    break;
                }
                catch(Exception ex)
                {
                    if (ex is StructureException || ex is EncodingException )
                    {
                        Console.WriteLine(ex.Message + ". Press any button to continue");
                    }
                    else Console.WriteLine("Wrong address, try again, please. Press any button to continue");
                    button = Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }              
            } while (true);

            Console.WriteLine();
            Console.WriteLine("Program was finished");
        }
    }
}

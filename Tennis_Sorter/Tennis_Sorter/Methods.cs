
namespace Tennis_Sorter
{
    public static class Methods
    {
        public static Player[] GetMenArray(string filename)
        {
            var max = 0;
            var men = new List<string>();
            using(var sr = new StreamReader(filename))
            {
                var s = sr.ReadLine();
                while(s != null)
                {
                    men.Add(s);
                    s = sr.ReadLine();
                }            
            }
            var result = men.ToArray();

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = RemoveEmptyEntries(result[i]);
                if(i >= 0 && i <= 8) result[i] = result[i].Substring(2);
                else if(i >= 9 && i <= 99) result[i] = result[i].Substring(3);
                else result[i] = result[i].Substring(4);
                result[i] = FixLine(result[i]);
                result[i] = result[i].Replace("  ", " ");
            }

            var players = new Player[result.Length];
            for (var i = 0; i < players.Length; i++)
            {
                var mas = result[i].Split(' ');
                string name = mas[0], rating = mas[mas.Length - 1];
                for (var j = 1; j < mas.Length - 1; j++)
                {
                    name += " " + mas[j];
                }
                if (name.Length > max) max = name.Length;
                players[i] = new Player(name, double.Parse(rating));
            }

            return players;
        }

        public static void WriteArray(Player[] arr, string filename)
        {
        
            using (var sw = new StreamWriter(filename))
            {
                for (var i = 1; i < arr.Length; i++)
                {
                    
                    sw.WriteLine($"{i}. {arr[i-1]}");
                 
                }
            }
            
        }

        private static string RemoveEmptyEntries(string line)
        {
            while (line.Contains("  "))
            {
                line = line.Replace("  ", " ");
            }

            return line.Replace(". ", ".");
        }
        private static string FixLine(string line)
        {
            var k = 0;
            var new_line = "";
            for(var i = 0; i < line.Length; i++)
            {
                if ("0123456789".Contains(line[i]) && k == 0)
                {
                    new_line += " " + line[i];
                    k++;
                }
                else new_line += line[i];
            }

            return new_line;
        }
    }
}

using System.Security.Cryptography;

namespace Tennis_Sorter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var men = Methods.GetMenArray("men.txt");
            var women = Methods.GetMenArray("women.txt");
            var func = delegate (Player p1, Player p2)
            {
                if (p1.Rating < p2.Rating) return 1;
                if (p1.Rating > p2.Rating) return -1;
                return 0;
            };

            Array.Sort(men, (p1, p2) => func(p1, p2));
            Array.Sort(women, ( p1,  p2) => func(p1, p2));

            Console.WriteLine("Men single's: ");
            Console.WriteLine();
            Methods.WriteArray(men, "men_output.txt");
            Console.WriteLine();
            Console.WriteLine("Women single's: ");
            Console.WriteLine();
            Methods.WriteArray(women, "women_output.txt");
        }
    }
}
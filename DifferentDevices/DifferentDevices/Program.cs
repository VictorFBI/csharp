namespace DifferentDevices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (true)
            {
                if (menu.IsFinished()) break;
            }
        }
    }
}
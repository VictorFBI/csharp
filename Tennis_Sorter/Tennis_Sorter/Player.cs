namespace Tennis_Sorter
{
    public class Player
    {
        private static int _max, _k;
        public Player(string name, double rating)
        {
            if(name.Length > _max) _max = name.Length;
            Name = name;
            Rating = rating;
        }
       
        private string Name { get; }
        public double Rating { get; }

        public override string ToString()
        {
            return $"{Name}       {Rating}";
        }

    }
}

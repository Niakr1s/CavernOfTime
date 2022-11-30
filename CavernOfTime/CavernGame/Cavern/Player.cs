namespace CavernOfTime
{
    public class Player
    {
        public bool FountainVisited { get; set; } = false;

        public bool IsDead { get; set; } = false;

        public Weapon Weapon { get; set; } = new Weapon(10);
    }
}

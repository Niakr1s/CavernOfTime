namespace CavernOfTime
{
    public class Player
    {

        public bool FountainVisited { get; set; } = false;



        private bool _isDead = false;
        public bool IsDead
        {
            get { return _isDead || Health.IsDead; }
            set => _isDead = value;
        }



        public Weapon Weapon { get; set; } = new Axe(10);

        public Health Health { get; set; } = new Health(100);
    }
}

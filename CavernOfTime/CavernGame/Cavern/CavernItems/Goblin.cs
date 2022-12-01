namespace CavernOfTime
{
    public class Goblin : Mob
    {
        public Goblin() : base(new Health(50))
        {
            MeleeWeapon = new Axe(25);
            RangedWeapon = new Bow(10, 2);
        }
    }
}
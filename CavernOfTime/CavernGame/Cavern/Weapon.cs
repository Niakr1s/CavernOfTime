namespace CavernOfTime
{
    #region Abstract classes 

    public abstract class Weapon
    {
        public Weapon(int damage, int range)
        {
            Damage = damage;

            if (range < 0) range = 0;
            Range = range;
        }


        public int Damage { get; }



        public int Range { get; }

        public bool IsRanged()
        {
            return Range != 0;
        }

        public override string ToString()
        {
            return $"{GetType().Name} with damage {Damage}";
        }
    }

    public abstract class MeleeWeapon : Weapon
    {
        public MeleeWeapon(int damage) : base(damage, 0)
        {
        }
    }

    public abstract class RangedWeapon : Weapon
    {
        public RangedWeapon(int damage, int range) : base(damage, range)
        {
        }
    }

    #endregion



    #region Real weapons

    public class Axe : MeleeWeapon
    {
        public Axe(int damage) : base(damage)
        {
        }
    }

    public class Bow : RangedWeapon
    {
        public Bow(int damage, int range) : base(damage, range)
        {
        }
    }

    #endregion
}
namespace CavernOfTime
{
    public class Health
    {

        public Health(int initialHealth)
        {
            _value = initialHealth;
        }



        private int _value;
        public int Value { get => _value; }

        public bool IsDead { get => Value <= 0; }



        public void TakeDamage(int dmg)
        {
            _value -= dmg;
        }
    }
}

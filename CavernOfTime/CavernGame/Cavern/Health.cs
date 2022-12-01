namespace CavernOfTime
{
    public class Health
    {

        public Health(int initialHealth)
        {
            _value = initialHealth;
            _maxHealth = initialHealth;
        }


        private int _maxHealth;


        public int Percent { get => (Value * 100) / _maxHealth; }



        private int _value;
        public int Value { get => _value; }

        public int MaxHealth { get => _maxHealth; }

        public bool IsDead { get => Value <= 0; }



        public void TakeDamage(int dmg)
        {
            _value -= dmg;
            if (_value < 0) { _value = 0; }
        }


        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

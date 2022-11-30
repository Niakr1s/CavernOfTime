namespace CavernOfTime
{
    public class Health
    {

        public Health(int initialHealth)
        {
            _value = initialHealth;
            _initialValue = initialHealth;
        }


        private int _initialValue;


        public int Percent { get => (Value * 100) / _initialValue; }



        private int _value;
        public int Value { get => _value; }

        public bool IsDead { get => Value <= 0; }



        public void TakeDamage(int dmg)
        {
            _value -= dmg;
            if (_value < 0) { _value = 0; }
        }
    }
}

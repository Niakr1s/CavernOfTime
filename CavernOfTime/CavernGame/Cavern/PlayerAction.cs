namespace CavernOfTime
{
    public class PlayerAction
    {
        public Direction? Direction { get; init; } = null;

        public Direction? ShootDirection { get; init; } = null;

        public bool WantInteract { get; init; } = false;



        public override string ToString()
        {
            if (Direction is Direction md)
            {
                return $"Move: {md}";
            }
            else if (ShootDirection is Direction sd)
            {
                return $"Shoot: {sd}";
            }
            else if (WantInteract)
            {
                return $"Interact";
            }
            throw new ArgumentException();
        }
    }
}
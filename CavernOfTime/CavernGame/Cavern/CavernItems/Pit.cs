namespace CavernOfTime
{
    public class Pit : CavernItem
    {
        public override bool Interact(Player player)
        {
            player.IsDead = true;
            return true;
        }
    }
}
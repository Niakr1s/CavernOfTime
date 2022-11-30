namespace CavernOfTime
{
    public class Fountain : CavernItem
    {
        private bool _isActive;
        public override bool IsActive => _isActive;

        public override bool Interact(Player player)
        {
            player.FountainVisited = true;
            _isActive = false;

            return true;
        }
    }
}
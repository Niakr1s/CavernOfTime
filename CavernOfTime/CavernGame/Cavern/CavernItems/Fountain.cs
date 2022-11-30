namespace CavernOfTime
{
    public class Fountain : CavernItem
    {
        private bool _isActive;
        public override bool IsActive => _isActive;

        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.FountainVisited = true;
            _isActive = false;

            return true;
        }
    }
}
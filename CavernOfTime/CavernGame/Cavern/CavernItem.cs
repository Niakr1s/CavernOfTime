namespace CavernOfTime
{
    public interface ICavernItem
    {
        /// <summary>
        /// Interats with player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>True, if interaction was successfull.</returns>
        bool Interact(Player player);

        bool IsActive { get; }
    }
    /// <summary>
    /// Item, contained in Cavern.
    /// </summary>
    public abstract class CavernItem : ICavernItem
    {
        public virtual bool IsActive { get; } = true;

        public abstract bool Interact(Player player);
    }

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
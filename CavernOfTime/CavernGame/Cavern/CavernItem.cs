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
    }
    /// <summary>
    /// Item, contained in Cavern.
    /// </summary>
    public abstract class CavernItem : ICavernItem
    {
        public abstract bool Interact(Player player);
    }

    public class Fountain : CavernItem
    {
        public override bool Interact(Player player)
        {
            player.FountainVisited = true;
            return true;
        }
    }
}
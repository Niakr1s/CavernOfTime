namespace CavernOfTime
{
    /// <summary>
    /// Item, contained in Cavern.
    /// </summary>
    public abstract class CavernItem
    {
        public virtual bool IsActive { get; } = true;

        public abstract bool Interact(Player player);
    }
}
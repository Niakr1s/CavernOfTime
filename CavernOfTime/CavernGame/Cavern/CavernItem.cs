namespace CavernOfTime
{
    /// <summary>
    /// Item, contained in Cavern.
    /// </summary>
    public abstract class CavernItem
    {
        public virtual bool IsActive { get; set; } = true;

        public virtual bool AutoInteract { get; } = true;

        /// <summary>
        /// Calls, when player means to interact with item.
        /// </summary>
        /// <param name="cavern"></param>
        /// <returns></returns>
        public abstract bool InteractWithPlayer(Cavern cavern);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns>False, if wasn't attacked</returns>
        public virtual bool ReceiveAttackFromPlayer(Weapon weapon)
        {
            return false;
        }
    }
}
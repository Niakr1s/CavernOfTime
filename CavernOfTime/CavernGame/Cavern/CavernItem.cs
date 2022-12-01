namespace CavernOfTime
{
    /// <summary>
    /// Item, contained in Cavern.
    /// </summary>
    public abstract class CavernItem
    {
        public virtual bool IsActive { get; set; } = true;

        public virtual bool AutoInteract { get; } = true;

        public Position? Position { get; set; } = null;

        /// <summary>
        /// Calls, when player means to interact with item.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="logMsg">Should be not null, if interaction was done.</param>
        /// <returns></returns>
        public virtual bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            logMsg = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="logMsg">Should be not null, if interaction was done.</param>
        /// <returns>False, if wasn't attacked</returns>
        public virtual bool ReceiveAttackFromPlayer(Weapon weapon, out string? logMsg)
        {
            logMsg = null;
            return false;
        }
    }
}
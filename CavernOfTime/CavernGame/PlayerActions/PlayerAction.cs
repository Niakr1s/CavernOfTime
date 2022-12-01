namespace CavernOfTime.PlayerActions
{
    public abstract class PlayerAction
    {
        public bool WantInteract { get; init; } = false;


        public override string ToString()
        {
            return this switch
            {
                PlayerMoveAction a => $"Move: {a.Direction}",
                PlayerShootAction a => $"Shoot: {a.Direction}",
                PlayerInteractRequestAction => $"Interact",
                _ => $"Unknown PlayerAction: {this}",
            };
        }
    }

}
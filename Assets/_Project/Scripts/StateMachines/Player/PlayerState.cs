public abstract class PlayerState
{
    public abstract void EnterState(PlayerStateManager manager);
    public abstract void UpdateState(PlayerStateManager manager);
    public abstract void ExitState(PlayerStateManager manager);
}

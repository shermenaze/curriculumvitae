public abstract class PlayerBaseState
{
    protected readonly PlayerController _controller;

    protected PlayerBaseState(PlayerController controller)
    {
        _controller = controller;
    }
    
    public abstract void EnterState();

    public abstract void Update();

    public abstract void ExitState();
}
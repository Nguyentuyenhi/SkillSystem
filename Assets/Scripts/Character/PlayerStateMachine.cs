// 1. PlayerStateMachine.cs - Quản lý trạng thái hiện tại
public class PlayerStateMachine
{
    public PlayerBaseState CurrentState { get; private set; }

    public void Initialize(PlayerBaseState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerBaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

// 2. PlayerBaseState.cs - Khuôn mẫu cho mọi trạng thái
public abstract class PlayerBaseState
{
    protected PlayerController core;
    protected PlayerStateMachine stateMachine;

    // Tên animation mà state này sẽ chạy
    protected string animName;

    public PlayerBaseState(PlayerController core, PlayerStateMachine stateMachine, string animName)
    {
        this.core = core;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        // Tự động chạy animation khi vào State
        core.AnimController.PlayAnim(animName, true);
    }

    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
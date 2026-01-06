using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController core, PlayerStateMachine sm, string animName) : base(core, sm, animName) { }

    public override void Enter()
    {
        base.Enter();
        core.Rb.linearVelocity = Vector2.zero; // Dừng hẳn nhân vật
    }

    public override void LogicUpdate()
    {
        // 1. Chuyển sang Di chuyển
        if (core.InputX != 0)
        {
            stateMachine.ChangeState(core.MoveState);
        }

        // 2. Chuyển sang Đánh
        if (Input.GetKeyDown(KeyCode.J)) // Giả sử J là đánh
        {
            stateMachine.ChangeState(core.AttackState);
        }
    }
}
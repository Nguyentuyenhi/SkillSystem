using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController core, PlayerStateMachine sm, string animName) : base(core, sm, animName) { }

    public override void LogicUpdate()
    {
        // 1. Kiểm tra nếu dừng di chuyển -> Về Idle
        if (core.InputX == 0)
        {
            stateMachine.ChangeState(core.IdleState);
            return;
        }

        // 2. Kiểm tra tấn công
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(core.AttackState);
            return;
        }

        if (core.IsRunning)
        {
            core.AnimController.PlayAnim("Run", true);
        }
        else
        {
            core.AnimController.PlayAnim("Walk", true);
        }

        // 4. Quay mặt nhân vật
        if (core.InputX > 0) core.SkeletonAnim.skeleton.ScaleX = 1;
        else if (core.InputX < 0) core.SkeletonAnim.skeleton.ScaleX = -1;
    }

    public override void PhysicsUpdate()
    {
        // Tính toán tốc độ
        float speed = core.IsRunning ? core.RunSpeed : core.WalkSpeed;
        core.Rb.linearVelocity = new Vector2(core.InputX * speed, core.Rb.linearVelocity.y);
    }
}
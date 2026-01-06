using Spine;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool isAnimationFinished;

    public PlayerAttackState(PlayerController core, PlayerStateMachine sm, string animName) : base(core, sm, animName) { }

    public override void Enter()
    {
        core.Rb.linearVelocity = Vector2.zero; 
        isAnimationFinished = false;

        var track = core.SkeletonAnim.state.SetAnimation(0, animName, false);

        track.Complete += OnComplete;
    }

    private void OnComplete(TrackEntry trackEntry)
    {
        isAnimationFinished = true;
    }

    public override void LogicUpdate()
    {
        if (isAnimationFinished)
        {
            // Đánh xong thì về Idle (hoặc logic combo nếu muốn mở rộng)
            stateMachine.ChangeState(core.IdleState);
        }
    }

    // Hủy đăng ký sự kiện để tránh lỗi bộ nhớ
    public override void Exit()
    {
        core.SkeletonAnim.state.Complete -= OnComplete;
    }
}
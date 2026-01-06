using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerController core, PlayerStateMachine sm, string animName) : base(core, sm, animName) { }

    public override void Enter()
    {
        // Chạy anim chết, không lặp lại
        core.AnimController.PlayAnim(animName, false);

        // Vô hiệu hóa vật lý
        core.Rb.linearVelocity = Vector2.zero;
        core.Rb.bodyType = RigidbodyType2D.Kinematic;

        // Tắt Collider nếu cần
    }

    // Không làm gì cả trong Update, nhân vật nằm im
    public override void LogicUpdate() { }
}
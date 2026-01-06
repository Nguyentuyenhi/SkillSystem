using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D Rb;
    public SkeletonAnimation SkeletonAnim; // Component của Spine

    [Header("Settings")]
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;

    // State Machine
    public PlayerStateMachine StateMachine { get; private set; }

    // Khai báo các State
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    // Biến phụ trợ
    [HideInInspector] public float InputX;
    [HideInInspector] public bool IsRunning;
    public bool IsDead = false;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        // Khởi tạo các State với tên Animation tương ứng trong Spine
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Walk"); // Mặc định là Walk
        AttackState = new PlayerAttackState(this, StateMachine, "Attack_Normal");
        DeathState = new PlayerDeathState(this, StateMachine, "Die");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (IsDead) return; // Chết rồi thì không nhận input

        // Đọc Input chung
        InputX = Input.GetAxisRaw("Horizontal");
        IsRunning = Input.GetKey(KeyCode.LeftShift);

        // Xử lý logic State hiện tại
        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();

        // Test chết
        if (Input.GetKeyDown(KeyCode.K)) Die();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();    }

    // Hàm hỗ trợ Animation (Wrapper)
    public class AnimationController
    {
        private SkeletonAnimation _spine;
        public AnimationController(SkeletonAnimation spine) { _spine = spine; }

        public void PlayAnim(string name, bool loop)
        {
            if (_spine.AnimationName == name) return;
            _spine.state.SetAnimation(0, name, loop);
        }
    }
    // Khởi tạo wrapper này trong Awake nhé (mình viết tắt ở đây cho gọn)
    public AnimationController AnimController;

    public void Die()
    {
        IsDead = true;
        StateMachine.ChangeState(DeathState);
    }
}
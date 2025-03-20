// PlayerController.cs
// 3Dキャラクターの移動処理を実装するクラス（Observerパターンでイベントを受信）
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    [Header("Attack Settings")]
    public WeaponBase equippedWeapon; // 装備中の武器
    public BasicAttack basicAttack; // 基本攻撃


    [Header("Components")]
    public Animator Animator { get; private set; }
    public Rigidbody rb { get; private set; }

    /// <summary>
    /// ステートパターンによるアニメーション管理
    /// </summary>
    public IPlayerState CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public AttackState AttackState { get; private set; }

    /// <summary>
    /// 状態管理用フラグ（外部入力などで更新される）
    /// </summary>
    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // 状態インスタンスの生成
        IdleState = new IdleState();
        WalkState = new WalkState();
        AttackState = new AttackState();

        basicAttack = GetComponent<BasicAttack>();
        if (basicAttack == null)
        {
            basicAttack = gameObject.AddComponent<BasicAttack>();
        }
    }

    private void Start()
    {
        // 初期状態はアイドル
        TransitionToState(IdleState);

        // 初期武器を設定（例：インスペクターから設定）
        if (equippedWeapon == null)
        {
            // デフォルト武器を設定
            //equippedWeapon = ScriptableObject.CreateInstance<Sword>();
            //(equippedWeapon as Sword).damage = 5f;
            //(equippedWeapon as Sword).range = 1.5f;
            Debug.LogError("No weapon equipped!");
        }
    }

    private void OnEnable()
    {
        // 移動入力イベントに登録（Observerパターン）
        GameEventManager.OnMoveInput += HandleMoveInput;
    }

    private void OnDisable()
    {
        GameEventManager.OnMoveInput -= HandleMoveInput;
    }

    // 発行された移動入力イベントを処理し、キャラクターを移動させる
    private void HandleMoveInput(Vector3 moveDirection)
    {
        // 入力の大きさに応じて移動フラグを設定
        IsMoving = moveDirection.magnitude > 0.1f;
        _moveDirection = moveDirection;
    }

    private void Update()
    {
        // 現在の状態の更新を呼び出し、アニメーション遷移などを管理
        CurrentState?.UpdateState(this);

        // ※ IsAttacking は外部入力やタイミングで更新する前提です
        // 攻撃状態の更新
        if (IsAttacking)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    // 状態遷移を行うメソッド
    public void TransitionToState(IPlayerState newState)
    {
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    private void MoveCharacter()
    {
        if (_moveDirection.magnitude > 0.1f)
        {
            // 移動方向へのスムーズな回転
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // 加速処理
            _currentVelocity = Vector3.Lerp(_currentVelocity, _moveDirection.normalized * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // 減速処理
            _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Rigidbody を使った移動
        rb.linearVelocity = _currentVelocity;
    }

    // 攻撃処理
    public void Attack()
    {
        // 武器を使用
        equippedWeapon.Use();
        // 基本攻撃を実行
        basicAttack.Attack();
        // 攻撃アニメーションの再生など
    }

    // 攻撃ボタン押下時の処理
    public void OnAttackButtonClicked()
    {
        IsAttacking = true;
        // 攻撃状態に遷移
        TransitionToState(AttackState);
    }
}

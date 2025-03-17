// PlayerController.cs
// 3Dキャラクターの移動処理を実装するクラス（Observerパターンでイベントを受信）
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator Animator { get; private set; }

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

    private void Awake()
    {
        Animator = GetComponent<Animator>();

        // 状態インスタンスの生成
        IdleState = new IdleState();
        WalkState = new WalkState();
        AttackState = new AttackState();
    }

    private void Start()
    {
        // 初期状態はアイドル
        TransitionToState(IdleState);
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

        // シンプルな移動処理（Translate を使用）
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        Debug.Log("PlayerController: Moving with direction " + moveDirection);
    }

    private void Update()
    {
        // 現在の状態の更新を呼び出し、アニメーション遷移などを管理
        CurrentState?.UpdateState(this);

        // ※ IsAttacking は外部入力やタイミングで更新する前提です
    }

    // 状態遷移を行うメソッド
    public void TransitionToState(IPlayerState newState)
    {
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}

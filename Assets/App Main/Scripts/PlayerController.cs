// PlayerController.cs
// 3Dキャラクターの移動処理を実装するクラス（Observerパターンでイベントを受信）
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

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
        // シンプルな移動処理（Translate を使用）
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        Debug.Log("PlayerController: Moving with direction " + moveDirection);
    }
}

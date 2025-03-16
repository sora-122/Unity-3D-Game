// ContinuousMoveCommand.cs
// タッチパネルからの2D入力を3D空間の移動方向に変換し、Observerパターンに基づいてイベント発行するコマンド
using UnityEngine;

public class ContinuousMoveCommand : IMoveCommand
{
    // パラメータ無し Execute は今回未使用
    public void Execute() { }

    // タッチ操作から得た2D方向を3D方向（X-Z平面）に変換し、移動イベントを発行
    public void Execute(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
        Debug.Log("ContinuousMoveCommand: moveDirection = " + moveDirection);
        // Observerパターンにより、移動入力イベントを発行
        GameEventManager.PublishMoveInput(moveDirection);
    }
}

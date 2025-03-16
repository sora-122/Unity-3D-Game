// GameEventManager.cs
// ゲーム全体のイベント（Observerパターン）を管理する静的クラス
using System;
using UnityEngine;

public static class GameEventManager
{
    // 3Dキャラクターの移動入力を通知するイベント
    public static event Action<Vector3> OnMoveInput;

    // 移動入力イベントを発行するメソッド
    public static void PublishMoveInput(Vector3 moveDirection)
    {
        OnMoveInput?.Invoke(moveDirection);
    }
}

// AttackState.cs
// 攻撃状態
using UnityEngine;

public class AttackState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Enter Attack State");
        player.Animator.SetBool("IsAttacking", true);
        player.IsAttacking = true;
    }

    public void UpdateState(PlayerController player)
    {
        // 攻撃アニメーションが終了したら、アイドル状態に戻る
        if (player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.IsAttacking = false;
            player.TransitionToState(player.IdleState);
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exit Attack State");
        player.Animator.SetBool("IsAttacking", false);
    }
}

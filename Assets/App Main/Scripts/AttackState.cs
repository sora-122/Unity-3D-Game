public class AttackState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.Animator.Play("Attack");
    }

    public void UpdateState(PlayerController player)
    {
        if (!player.IsAttacking)
        {
            if (player.IsMoving)
            {
                player.TransitionToState(player.WalkState);
            }
            else
            {
                player.TransitionToState(player.IdleState);
            }
        }
    }

    public void ExitState(PlayerController player)
    {
        // 必要に応じて、状態終了時の処理を記述
    }
}

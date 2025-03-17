public class IdleState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.Animator.Play("Idle");
    }

    public void UpdateState(PlayerController player)
    {
        if (player.IsMoving)
        {
            player.TransitionToState(player.WalkState);
        }
        else if (player.IsAttacking)
        {
            player.TransitionToState(player.AttackState);
        }
    }

    public void ExitState(PlayerController player)
    {
        // 必要に応じて、状態終了時の処理を記述
    }
}

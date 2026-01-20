using UnityEngine;

public class Player_DashState : PlayerState
{
    private float originalGravityScales;
    private int dashDir;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        

        originalGravityScales = rb.gravityScale;
        rb.gravityScale = 0;
        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScales;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();

        player.SetVelocity(player.dashSpeed * dashDir, 0);

        if (stateTimer <0)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    private void CancelDashIfNeeded()
    {
        if(player.wallDetected)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }
}

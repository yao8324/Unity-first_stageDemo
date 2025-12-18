using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.isGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if(Input.GetButtonDown("Jump") && player.isGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        
    }
}

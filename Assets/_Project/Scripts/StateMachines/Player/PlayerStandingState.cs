using UnityEngine;

[System.Serializable]
public class PlayerStandingState : PlayerState
{
    [SerializeField] private float _walkSpeed = 1;
    public override void EnterState(PlayerStateManager manager)
    {
        manager.MoveSpeed = _walkSpeed;
    }

    public override void ExitState(PlayerStateManager manager)
    {

    }

    public override void UpdateState(PlayerStateManager manager)
    {

    }
}

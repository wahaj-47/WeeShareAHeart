using UnityEngine;
using DG.Tweening;

public class PlayerHumanRoamState : PlayerHumanBaseState
{

    public PlayerHumanRoamState(PlayerStateManager manager) : base(manager) { }

    public override void UpdateState()
    {
        base.UpdateState();

        ((PlayerStateManager)manager).animator.SetBool("isMoving", base.movement.x != 0);

        if (((PlayerStateManager)manager).interactable != null)
        {
            if(Input.GetButtonDown("Fire" + ((PlayerStateManager)manager).playerId))
            {
                ((PlayerStateManager)manager).interactable.Interact((PlayerStateManager)manager);
            }
        }
        else if (Input.GetButtonUp("Fire" + ((PlayerStateManager)manager).playerId))
        {
            ((PlayerStateManager)manager).SwitchState(((PlayerStateManager)manager).HumanAimState);
        }

        if (((PlayerStateManager)manager).utils.IsGrounded())
        {
            if(movement.x != 0)
            {
                float dot = ((PlayerStateManager)manager).utils.CheckSlope();

                if (dot < 0) DOTween.To(() => ((PlayerStateManager)manager).moveSpeed, x => ((PlayerStateManager)manager).moveSpeed = x, 100f * Mathf.Abs(dot * 1.75f), 0.05f);
                else if (dot > 0) DOTween.To(() => ((PlayerStateManager)manager).moveSpeed, x => ((PlayerStateManager)manager).moveSpeed = x, 15f, 0.05f);
                else DOTween.To(() => ((PlayerStateManager)manager).moveSpeed, x => ((PlayerStateManager)manager).moveSpeed = x, 30f, 0.05f);
            }
            
        } else ((PlayerStateManager)manager).moveSpeed = 0f;
    }

}

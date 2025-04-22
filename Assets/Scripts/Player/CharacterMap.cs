using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;
    }
    public void OnDash(InputValue value)
    {
        if (!dashIsOn && dashStartTime + dashTime + dashCooldown < Time.time)
        {
            dashIsOn = true;
            dashStartTime = Time.time;
        }
    }
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            isAttacking = true;
            attackIsCharging = true;
            attackStartTime = Time.time;
        }

        if (!value.isPressed)
        {
            attackIsCharging = false;
        }
    }
}


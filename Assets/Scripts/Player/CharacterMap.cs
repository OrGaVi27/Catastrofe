using Unity.VisualScripting;
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
    public void OnDash()
    {
        if (!dashIsOn && dashStartTime + dashTime + dashCooldown < Time.time && controller.isGrounded)
        {
            dashIsOn = true;
            dashStartTime = Time.time;
        }
    }
    public void OnAttack(InputValue value)
    {
        if (value.isPressed && controller.isGrounded && !isAttacking)
        {
            isAttacking = true;
            attackIsCharging = true;
            attackStartTime = Time.time;
            anim.SetBool("IsCharging", true);
            
        }

        if (!value.isPressed)
        {
            if (element != 0 && baseStats.currentMana > 0) baseStats.currentMana--;
            attackIsCharging = false;
            anim.SetBool("IsCharging", false);
        }
    }

    public void OnElementChange(InputValue value)
    {
        Vector2 elementImput = value.Get<Vector2>();

        //Debug.Log(elementImput);
        if (baseStats.currentMana <= 0) return;
        switch (elementImput)
        {

            //Fuego
            case { x: 0, y: 1 }:
                if (element == 1)
                {
                    element = 0;
                }
                else
                {
                    element = 1;
                }
                break;

            //Roca
            case { x: 1, y: 0 }:
                if (element == 2)
                {
                    element = 0;
                }
                else
                {
                    element = 2;
                }
                break;

            //Rayo
            case { x: 0, y: -1 }:
                if (element == 3)
                {
                    element = 0;
                }
                else
                {
                    element = 3;
                }
                break;

            //Agua
            case { x: -1, y: 0 }:
                if (element == 4)
                {
                    element = 0;
                }
                else
                {
                    element = 4;
                }
                break;
        }
    }
}


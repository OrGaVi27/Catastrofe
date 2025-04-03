using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    private void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        moveVector.x = inputVector.x ;
        moveVector.z = inputVector.y ;
    }
    private void OnDash(InputValue value)
    {
        if (!dashIsOn)
        {
            dashIsOn = true;
            StartCoroutine(Dash());
        }
    }
}

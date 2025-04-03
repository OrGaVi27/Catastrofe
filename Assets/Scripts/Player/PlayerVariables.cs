using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerStateManager
{
    #region ConCreteStates
    public WalkState walkState = new WalkState();
    public IdleState idleState = new IdleState();
    public FallState fallState = new FallState();
    #endregion


    public CharacterController controller;
    public PlayerInput input;
    public PlayerBaseState currentState;

    public Vector3 moveVector;
    public Vector2 inputVector;
    public float playerSpeed = 10;
    public float playerRotateSpeed = 1000;
    
    public bool dashIsOn = false;
    public float dashSpeed;
    public float dashTime;

    private Vector3 gravityVector;
    private Vector3 isometricImput;
    private Matrix4x4 referenceMatrix;
}

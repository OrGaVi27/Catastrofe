using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerStateManager
{
    #region States
    [HideInInspector] public WalkState walkState = new WalkState();
    [HideInInspector] public IdleState idleState = new IdleState();
    [HideInInspector] public FallState fallState = new FallState();
    [HideInInspector] public DashState dashState = new DashState();
    [HideInInspector] public AttackState attackState = new AttackState();
    #endregion

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput input;
    public PlayerBaseState currentState;


    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public Vector2 inputVector;
    [Header("Move")]
    public float playerSpeed;
    public float playerRotateSpeed;

    [Space]
    [Header("Dash")]
    public bool dashIsOn = false;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    protected float dashStartTime;

    [Space]
    [Header("Attack")]
    public bool isAttacking = false;
    public bool attackIsCharging = false;
    public float attackChargeTime;
    public float attackStartTime;
    public float attackMovSpeed;

    private Vector3 gravityVector;
    private Vector3 isometricImput;
    private Matrix4x4 referenceMatrix;
    [HideInInspector] public BasePlayerStats baseStats;
}

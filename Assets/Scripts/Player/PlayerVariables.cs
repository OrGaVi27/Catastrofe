using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerStateManager
{
    public bool rangeCharacter = false;


    #region States
    [HideInInspector] public WalkState walkState = new WalkState();
    [HideInInspector] public IdleState idleState = new IdleState();
    [HideInInspector] public FallState fallState = new FallState();
    [HideInInspector] public DashState dashState = new DashState();
    [HideInInspector] public AttackState attackState = new AttackState();
    #endregion

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public PlayerBaseState currentState;


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
    public GameObject cutEffect;
    public List<Material> cutEffects;
    protected float dashStartTime;

    [Space]
    [Header("Attack")]
    public GameObject[] Weapons;
    public Material[] weaponMaterials;
    public GameObject[] RangedProjectiles;
    public GameObject aimAid;
    public GameObject aimPoint;
    public bool isAttacking = false;
    public bool attackIsCharging = false;
    public float attackChargeTime;
    [HideInInspector] public float attackStartTime;
    [HideInInspector] public bool chargedAttack = false;
    public float attackMovSpeed;
    public float maxComboDelay = 0;
    public CinemachineVirtualCamera virtualCamera;
    [HideInInspector] public static int noOfAttacks;

    [Space]
    [Header("Animation")]
    public Animator anim;

    [Space]
    [Header("Sound")]
    [SerializeField] public AudioClip attackSound;
    [SerializeField] public AudioClip ChargeAttackSound;
    [SerializeField] public AudioClip ChargingAttack;
    [SerializeField] public AudioClip DashSound;
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public AudioClip waterSound;
    [SerializeField] public AudioClip electricSound;
    [SerializeField] public AudioClip stoneSound;
    public bool dashSoundPlayed = false;
    public bool hasPlayedAttackSound = false;
    public bool chargingSoundPlayed = false;
    


    private Vector3 gravityVector;
    private Vector3 isometricImput;
    private Matrix4x4 referenceMatrix;
    [HideInInspector] public BasePlayerStats baseStats;
    public int element = 0;
}

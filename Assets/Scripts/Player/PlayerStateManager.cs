using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip ChargeAttackSound;
    [SerializeField] private AudioClip ChargingAttack;
    [SerializeField] private AudioClip DashSound;
    private bool dashSoundPlayed = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();

        gravityVector = new Vector3(0, -15f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        baseStats = GetComponent<BasePlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != fallState && !controller.isGrounded)
        {
            SwitchState(fallState);
        }

        currentState.UpdateState(this);

        ApplyGravity();
        AttackAnimUpdate();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    #region Movement

    public void ApplyGravity()
    {
        controller.Move(gravityVector * Time.deltaTime);
    }

    public void Move()
    {
        referenceMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        isometricImput = referenceMatrix.MultiplyPoint3x4(moveVector);

        controller.Move(isometricImput * playerSpeed * Time.deltaTime);

        Rotate();
    }

    public void Move(float speed)
    {
        referenceMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        isometricImput = referenceMatrix.MultiplyPoint3x4(moveVector);

        controller.Move(isometricImput * speed * Time.deltaTime);

        Rotate();
    }

    public void Dash()
{
    if (Time.time < dashStartTime + dashTime)
    {
        controller.Move(transform.forward * dashSpeed * Time.deltaTime);

        if (!dashSoundPlayed) 
        {
            ControladorSonido.Instance.EjecutarSonido(DashSound);
            dashSoundPlayed = true; 
        }
    }
    else
    {
        dashIsOn = false;
        dashSoundPlayed = false; 
    }
}

    public void Rotate()
    {
        if (isometricImput.magnitude == 0) return;

        var rotation = Quaternion.LookRotation(isometricImput);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, playerRotateSpeed * Time.deltaTime);
    }

    private float lastAttackSoundTime = 0f;
public float attackSoundCooldown = 0.3f; // Tiempo mínimo entre sonidos (ajustable)

public void Attack()
{
    if (noOfAttacks == 1)
    {
        anim.SetBool("Attack1", true);

        
        if (Time.time - lastAttackSoundTime >= attackSoundCooldown)
        {
            Debug.Log("Reproduciendo sonido de ataque");
            ControladorSonido.Instance.EjecutarSonido(attackSound);
            lastAttackSoundTime = Time.time;
        }
    }

        if (noOfAttacks >= 2 &&
            anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f &&
            anim.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
        
    }

        if (attackChargeTime + attackStartTime > Time.time)
        {
            anim.SetBool("StrongAttack", false);
    

        }
        else
        {
            anim.SetBool("StrongAttack", true);
            ControladorSonido.Instance.EjecutarSonido(ChargingAttack);
            

        }   

    if (anim.GetCurrentAnimatorStateInfo(1).IsName("-"))
    {
        isAttacking = false;
    }
}


    public void AttackAnimUpdate()
    {

        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
        {
            anim.SetBool("Attack1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("Attack2"))
        {
            anim.SetBool("Attack2", false);
            noOfAttacks = 0;
        }

        if(Time.time - attackInputTime > maxComboDelay)
        {
            noOfAttacks = 0;
        }
    }

    #endregion
}

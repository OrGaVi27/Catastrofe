using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager : MonoBehaviour
{
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

    public void Dash()
    {
        if (Time.time < dashStartTime + dashTime)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
        }
        else
        {
            dashIsOn = false;
        }
    }

    public void Rotate()
    {
        if (isometricImput.magnitude == 0) return;

        var rotation = Quaternion.LookRotation(isometricImput);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, playerRotateSpeed * Time.deltaTime);
    }

    public void Attack()
    {
        if (attackChargeTime + attackStartTime > Time.time)
        {
            Debug.Log("normal attack");
        }
        else
        {
            Debug.Log("STRONG ATTACK!");
        }

        isAttacking = false;
    }

    #endregion
}

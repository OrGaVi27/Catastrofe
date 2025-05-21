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

        if (baseStats.currentMana <= 0 && currentState != attackState) element = 0;

        ElementControll();
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
        anim.SetInteger("State", 1);
        anim.SetFloat("RunMultiplier", moveVector.magnitude);

        controller.Move(isometricImput * playerSpeed * Time.deltaTime);

        Rotate();
    }

    public void Move(float speed)
    {
        referenceMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        isometricImput = referenceMatrix.MultiplyPoint3x4(moveVector);
        anim.SetInteger("State", 1);
        anim.SetFloat("RunMultiplier", moveVector.magnitude);

        controller.Move(isometricImput * speed * Time.deltaTime);

        Rotate();
    }

    public void Dash()
    {
        if (Time.time <= dashStartTime + dashTime)
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


        if (noOfAttacks == 1)
        {
            anim.SetBool("Attack1", true);
        }/* 
        if (noOfAttacks >= 2 && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
        } */

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
        }/* 
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("Attack2"))
        {
            anim.SetBool("Attack2", false);
            noOfAttacks = 0;
        } */

        if (Time.time - attackInputTime > maxComboDelay)
        {
            noOfAttacks = 0;
        }
    }

    public void ElementControll()
    {

        ColorUtility.TryParseHtmlString("#EF4A4A", out rojoPersonalizado);
        ColorUtility.TryParseHtmlString("#B07C66", out marronPersonalizado);
        ColorUtility.TryParseHtmlString("#BDBB6C", out amarilloPersonalizado);
        ColorUtility.TryParseHtmlString("#525FEF", out azulPersonalizado);

        switch (element)
        {
            case 0:
                cutEffect.GetComponent<MeshRenderer>().material = cutEffects[0];
                elementSpark.SetActive(false);
                break;

            case 1:
                cutEffect.GetComponent<MeshRenderer>().material = cutEffects[1];
                elementSpark.SetActive(true);
                var main1 = elementSpark.GetComponent<ParticleSystem>().main;
                main1.startColor = rojoPersonalizado;
                break;

            case 2:
                cutEffect.GetComponent<MeshRenderer>().material = cutEffects[2];
                elementSpark.SetActive(true);
                var main2 = elementSpark.GetComponent<ParticleSystem>().main;
                main2.startColor = marronPersonalizado;
                break;

            case 3:
                cutEffect.GetComponent<MeshRenderer>().material = cutEffects[3];
                elementSpark.SetActive(true);
                var main3 = elementSpark.GetComponent<ParticleSystem>().main;
                main3.startColor = amarilloPersonalizado;
                break;

            case 4:
                cutEffect.GetComponent<MeshRenderer>().material = cutEffects[4];
                elementSpark.SetActive(true);
                var main4 = elementSpark.GetComponent<ParticleSystem>().main;
                main4.startColor = azulPersonalizado;
                break;
        }
    }

    public float DamageOutput()
    {
        if (anim.GetBool("StrongAttack"))
        {
            return baseStats.damage * baseStats.DamageMultiplier;
        }
        else
        {
            return baseStats.damage;
        }
    }

    public void CameraShake()
    {
        
    }
    #endregion
}

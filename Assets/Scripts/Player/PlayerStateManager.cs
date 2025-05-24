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

        if (rangeCharacter)
        {
            Weapons[0].SetActive(false);
            Weapons[1].SetActive(true);
            anim.SetBool("IsRanged", true);
            attackMovSpeed = 0f;
        }
        else
        {
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(false);
            anim.SetBool("IsRanged", false);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != fallState && !controller.isGrounded)
        {
            SwitchState(fallState);
        }

        if(anim.GetCurrentAnimatorStateInfo(2).IsName("INVENCIBLE") || anim.GetCurrentAnimatorStateInfo(2).IsName("TakeDamage"))
        {
            baseStats.isInvencible = true;
        }
        else
        {
            baseStats.isInvencible = false;
        }

        currentState.UpdateState(this);

        if (baseStats.currentMana <= 0 && currentState != attackState) element = 0;

        ElementControll();
        ApplyGravity();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    #region Functions


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

        if (rangeCharacter)
        {
            anim.SetFloat("RunMultiplier", moveVector.magnitude * 0f);
        }
        else
        {
            anim.SetFloat("RunMultiplier", moveVector.magnitude);
        }
        

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
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("-"))
        {
            isAttacking = false;
        }

        if (anim.GetBool("StrongAttack"))
        {
            //Sonido ataque fuerte
        }
        else
        {
            //Sonido ataque normal
        }
    }

    public void ElementControll()
    {
        int weapon = 0;
        if (rangeCharacter) weapon = 1;

            switch (element)
            {
                case 0:
                    cutEffect.GetComponent<MeshRenderer>().material = cutEffects[0];
                    Weapons[weapon].GetComponent<MeshRenderer>().material = weaponMaterials[weapon];
                    break;

                case 1:
                    cutEffect.GetComponent<MeshRenderer>().material = cutEffects[1];
                    Weapons[weapon].GetComponent<MeshRenderer>().material = weaponMaterials[2];
                    break;

                case 2:
                    cutEffect.GetComponent<MeshRenderer>().material = cutEffects[2];
                    Weapons[weapon].GetComponent<MeshRenderer>().material = weaponMaterials[3];
                    break;

                case 3:
                    cutEffect.GetComponent<MeshRenderer>().material = cutEffects[3];
                    Weapons[weapon].GetComponent<MeshRenderer>().material = weaponMaterials[4];
                    break;

                case 4:
                    cutEffect.GetComponent<MeshRenderer>().material = cutEffects[4];
                    Weapons[weapon].GetComponent<MeshRenderer>().material = weaponMaterials[5];
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
    #endregion
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MageBehaviour : BaseEnemyStats
{
    [Header("Patroll")]
    public Transform[] patrollPoints;
    Vector3 patrollTarget;
    int patrollIndex;

    [Space]
    [Header("Team")]
    public GameObject[] teamMembers = new GameObject[5];
    public bool fearMode = false;

    [Space]
    [Header("Vision Areas")]
    public EnemyVisionArea visionRange;
    public EnemyVisionArea defenseRange;
    public EnemyVisionArea safeSpaceRange;

    [Space]
    [Header("Healing")]
    public float healCooldown = 5f;
    public float healAmount = 15f;
    private bool healReady = false;
    private float lastTimeHealed = 0f;
    public float fleeSpeedMult;

    [Space]
    [Header("Animation")]
    public Animator anim;
    public GameObject staff;

    private GameObject player;

    [Header("Elementos")]
    public EnemigoElemental elemento;


    void Start()
    {
        //currentHealth = maxHealth - 50f;

        nmAgent = GetComponent<NavMeshAgent>();

        nmAgent.speed = movementSpeed;

        player = GameObject.FindGameObjectWithTag("Player");

        anim.SetInteger("State", 1);

        UpdateDestination();

        //PROVISIONAL
        // teamMembers[0] = gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        //Se usa para calcular el daño de los ataques
        if (elemento.elementoAleatorio == 4)
        {
            element = 0;
        }
        else
        {
            element = elemento.elementoAleatorio +1;
        }
        
        StaffColor();

        if (!defenseRange.playerInRange) nmAgent.speed = movementSpeed;

        if (visionRange.playerInRange && PlayerInLOS())
        {
            if (CheckTeam())
            {
                fearMode = false;
                elemento.elementoAleatorio = elemento.numeroElemento;

                if (currentHealth != maxHealth)
                {
                    if (defenseRange.playerInRange)
                    {
                        //Debug.Log("Fleeing");
                        Fleeing();
                    }
                    else
                    {
                        if (lastTimeHealed + healCooldown < Time.time)
                        {
                            healReady = true;
                        }
                    }
                }
                else
                {
                    //Debug.Log("Repositioning");
                    // Reposition the mage to a safe distance from the player
                    PositionSelf();
                }
            }
            else
            {
                fearMode = true;
                elemento.elementoAleatorio = 4;
                nmAgent.SetDestination(transform.position);
            }
        }
        else
        {
            elemento.elementoAleatorio = elemento.numeroElemento;

            //Debug.Log("Patrolling");
            nmAgent.stoppingDistance = 0;
            if (Vector3.Distance(nmAgent.pathEndPosition, transform.position) < 5f)
            {
                UpdateDestination();
            }

            Patroll();

        }


        if (healReady)
        {
            anim.SetInteger("State", 4);
            nmAgent.SetDestination(transform.position);
            lastTimeHealed = Time.time;
            healReady = false;
        }
        else if (nmAgent.velocity != Vector3.zero)
        {
            anim.SetInteger("State", 2);
        }
        else if (!fearMode)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 3);
        }
    }


    #region actions
    /// <summary>
    /// Returns true if you have any member on your team
    /// </summary>
    /// <returns></returns>
    public bool CheckTeam()
    {
        int vacantsEmpty = 0;
        foreach (GameObject vacant in teamMembers)
        {
            if (vacant == null)
            {
                vacantsEmpty++;
            }
        }

        if (vacantsEmpty >= 5)
        {
            return false;
        }

        return true;

    }

    /// <summary>
    /// Heals the mage and all its team members. It uses the Heal method from the BaseEnemyStats class to heal the mage and the Heal method from the BaseEnemyStats class to heal each team member.
    /// </summary>
    /// <param name="healAmount"></param>
    public void HealTeam(float healAmount)
    {
        Heal(healAmount);
        foreach (GameObject teamMember in teamMembers)
        {
            if (teamMember != null)
            {
                BaseEnemyStats enemyStats = teamMember.GetComponent<BaseEnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.Heal(healAmount);
                }
            }
        }
    }

    /// <summary>
    /// Repositions the mage to a safe distance from the player. It uses the NavMeshAgent component to move the mage to a new position.
    /// </summary>
    private void PositionSelf()
    {
        if (safeSpaceRange.playerInRange)
        {
            Fleeing();
        }

        if (defenseRange.playerInRange && !safeSpaceRange.playerInRange)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * movementSpeed);
            nmAgent.stoppingDistance = defenseRange.gameObject.transform.localScale.x / 2.1f;
            nmAgent.SetDestination(player.transform.position);
        }

        if (visionRange.playerInRange && !defenseRange.playerInRange && !safeSpaceRange.playerInRange)
        {
            nmAgent.stoppingDistance = defenseRange.gameObject.transform.localScale.x / 2.1f;
            nmAgent.SetDestination(player.transform.position);
        }
    }

    void Fleeing()
    {
        nmAgent.stoppingDistance = 0;
        nmAgent.speed = movementSpeed * fleeSpeedMult;

        Vector3 direction = (transform.position - player.transform.position).normalized;
        Vector3 newPosition = transform.position + direction * 10f;
        nmAgent.SetDestination(newPosition);
    }

    /// <summary>
    /// Checks if the player is in line of sight of the mage. It uses a raycast to check if there are any obstacles between the mage and the player.
    /// If there are no obstacles, it returns true. If there are obstacles, it returns false.
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>

    // private bool PlayerInLOS()
    // {
    //     RaycastHit LOS;
    //     Vector3 direction = player.transform.position - transform.position;
    //     Physics.Raycast(transform.position, direction, out LOS, Mathf.Infinity, (int)QueryTriggerInteraction.Ignore);

    //     if (LOS.collider.gameObject.CompareTag("Player"))
    //     {   
    //         return true;
    //     }

    //     return false;
    // }

    private bool PlayerInLOS()
    {
        RaycastHit LOS;
        Vector3 direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out LOS, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
        {

            if (LOS.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void Patroll()
    {
        if (Vector3.Distance(transform.position, patrollTarget) < 3f)
        {
            IteratePatrollPointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        patrollTarget = patrollPoints[patrollIndex].position;
        nmAgent.SetDestination(patrollTarget);
    }

    void IteratePatrollPointIndex()
    {
        patrollIndex++;
        if (patrollIndex == patrollPoints.Length)
        {
            patrollIndex = 0;
        }
    }

    void StaffColor()
    {
        var colorMult = 0.05f;
        switch (element)
        {
            case 0:
                staff.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(191, 191, 191));
                break;

            case 1:
                staff.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0) * colorMult); ;
                break;

            case 2:
                staff.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(112, 49, 0) * colorMult);
                break;

            case 3:
                staff.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 251, 0) * colorMult);
                break;

            case 4:
                staff.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 21, 255) * colorMult);
                break;
        }
    }
    #endregion


}

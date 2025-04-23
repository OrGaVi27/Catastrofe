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

    [Space]
    [Header("Vision Areas")]
    public EnemyVisionArea visionRange;
    public EnemyVisionArea defenseRange;
    public EnemyVisionArea safeSpaceRange;

    [Space]
    [Header("Healing")]
    public float healCooldown = 5f;
    public float healAmount = 15f;
    private bool healReady = true;
    private GameObject player;
    private NavMeshAgent nmAgent;


    void Start()
    {
        currentHealth = maxHealth - 50f;

        nmAgent = GetComponent<NavMeshAgent>();

        nmAgent.speed = movementSpeed;

        player = GameObject.FindGameObjectWithTag("Player");
        
        UpdateDestination();

        //PROVISIONAL
        // teamMembers[0] = gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        if (visionRange.playerInRange && PlayerInLOS())
        {
            if (CheckTeam())
            {
                if (currentHealth != maxHealth)
                {
                    if (defenseRange.playerInRange)
                    {
                        //Debug.Log("Fleeing");
                        Fleeing();
                    }
                    else
                    {
                        if (healReady)
                        {
                            // Heals the mage and all its team members
                            HealTeam(healAmount);
                            StartCoroutine(AbilityCooldown(healCooldown, healReady));
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
                Debug.Log("FearMode: activated");
                nmAgent.SetDestination(transform.position);
            }
        }
        else
        {
            //Debug.Log("Patrolling");
            nmAgent.stoppingDistance = 0;
            if (Vector3.Distance(nmAgent.pathEndPosition, transform.position) < 5f)
            {
                UpdateDestination();
            }

            Patroll();

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
        if (Vector3.Distance(transform.position, patrollTarget) < 1.5f)
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
    #endregion


    #region Coroutines
    IEnumerator AbilityCooldown(float cooldown, bool AbilityReady)
    {
        AbilityReady = false;

        yield return new WaitForSeconds(cooldown);

        AbilityReady = true;
    }
    #endregion
}
 
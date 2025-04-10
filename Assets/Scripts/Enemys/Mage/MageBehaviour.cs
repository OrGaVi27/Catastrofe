using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class MageBehaviour : BaseEnemyStats
{

    public EnemyVisionArea visionRange;
    public EnemyVisionArea defenseRange;
    public EnemyVisionArea safeSpaceRange;

    public float healCooldown = 5f;
    public float currentHealCooldown = 0;
    public float healAmount = 15f;
    private bool healReady = true;
    private GameObject player;

    public GameObject[] teamMembers = new GameObject[5];

    public NavMeshAgent nmAgent;

    void Start()
    {
        currentHealth = maxHealth;

        nmAgent = GetComponent<NavMeshAgent>();

        nmAgent.speed = movementSpeed;

        player = GameObject.FindGameObjectWithTag("Player");

        //PROVISIONAL
        teamMembers[0] = gameObject;


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
                        Debug.Log("Fleeing");
                    }
                    else
                    {
                        if (healReady)
                        {
                            // Heals the mage and all its team members
                            HealTeam(healAmount);
                            healReady = false;
                        }
                        else
                        {
                            HealCooldown();
                        }
                    }
                }
                else
                {
                    Debug.Log("Repositioning");
                    // Reposition the mage to a safe distance from the player
                    PositionSelf();
                }
            }
            else
            {
                Debug.Log("FearMode: activated");
            }
        }
        else
        {
            nmAgent.stoppingDistance = 0;
            Debug.Log("Patrolling");
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

    void HealCooldown()
    {
        if (healCooldown >= currentHealCooldown)
        {
            currentHealCooldown =- Time.deltaTime;
        }
        else
        {
            currentHealCooldown = 0;
            healReady = true;
        }
    }

    /// <summary>
    /// Repositions the mage to a safe distance from the player. It uses the NavMeshAgent component to move the mage to a new position.
    /// </summary>
    private void PositionSelf()
    {
        if (safeSpaceRange.playerInRange)
        {
            nmAgent.stoppingDistance = 0;

            Vector3 direction = (transform.position - player.transform.position).normalized;
            Vector3 newPosition = transform.position + direction * 10f;
            nmAgent.SetDestination(newPosition);
        }
        if(defenseRange.playerInRange && !safeSpaceRange.playerInRange)
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
    
    /// <summary>
    /// Checks if the player is in line of sight of the mage. It uses a raycast to check if there are any obstacles between the mage and the player.
    /// If there are no obstacles, it returns true. If there are obstacles, it returns false.
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    private bool PlayerInLOS()
    {
        RaycastHit LOS;
        Vector3 direction = player.transform.position - transform.position;
        Physics.Raycast(transform.position, direction, out LOS, Mathf.Infinity, (int)QueryTriggerInteraction.Ignore);
        if (LOS.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }
    #endregion
}

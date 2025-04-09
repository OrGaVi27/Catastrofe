using System.Collections;
using Unity.VisualScripting;
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

    public GameObject[] teamMembers = new GameObject[5];

    public NavMeshAgent nmAgent;

    void Start()
    {
        currentHealth = maxHealth;

        nmAgent = GetComponent<NavMeshAgent>();

        nmAgent.speed = movementSpeed;

        //PROVISIONAL
        teamMembers[0] = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (visionRange.playerInRange)
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

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;


        if (safeSpaceRange.playerInRange)
        {
            Vector3 direction = (transform.position - playerPosition).normalized;
            Vector3 newPosition = transform.position + direction * 2f;
            nmAgent.SetDestination(newPosition);
        }

        if (defenseRange.playerInRange && !safeSpaceRange.playerInRange)
        {
            nmAgent.SetDestination(gameObject.transform.position);
        }

        if (visionRange.playerInRange && !defenseRange.playerInRange && !safeSpaceRange.playerInRange)
        {
            if (PlayerInLOS(playerPosition))
            {
                nmAgent.SetDestination(playerPosition);
            }
        }
    }
    
    /// <summary>
    /// Checks if the player is in line of sight of the mage. It uses a raycast to check if there are any obstacles between the mage and the player.
    /// If there are no obstacles, it returns true. If there are obstacles, it returns false.
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    private bool PlayerInLOS(Vector3 playerPosition)
    {
        RaycastHit LOS;
        Vector3 direction = playerPosition - transform.position;
        Physics.Raycast(transform.position, direction, out LOS, Mathf.Infinity, (int)QueryTriggerInteraction.Ignore);
        if (LOS.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }
    #endregion
}

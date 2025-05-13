using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    [Header("Boss")]
    public Animator bossAnim;
    [Space]

    [Header("Elements")]
    public bool fire;
    public bool water;
    public bool electricity;
    public bool rock;

    public List<string> activeElements = new List<string>();
    public List<string> allElements = new List<string>();
    [Space]

    [Header("Life")]
    public int lifesCount;
    public Slider lifeSlider;

    public Image lifeColor;

    public float maxLiife;
    public float currentLiife;
    [Space]

    [Header("Pelea")]
    public float timeAttack;
    private float currentTimeAttack;
    public MeleeAtack melee;
    public DistanceAttack distance;

    public BossMovement move;

    private int MA;
    private int DA;

    public void Start()
    {
        melee = FindAnyObjectByType<MeleeAtack>();
        distance = FindAnyObjectByType<DistanceAttack>();
        move = GetComponent<BossMovement>();
    }

    public void Update()
    {
        if (move.nmAgent.velocity.magnitude > 0.5)
        {
            bossAnim.SetBool("move", true);
        }
        else
        {
            bossAnim.SetBool("move", false);
        }

        if (currentLiife <= 0)
        {
            if (lifesCount <= 4)
            {
                ChangeElement();

                lifesCount += 1;
                currentLiife = maxLiife;
            }
            else
            {
                BossDeath();
            }
        }
        else
        {
            TimeAttack();

            if (melee.meleeAtack == true)
            {
                Melee();

                move.Direccion();
            }
            else if (distance.distanceAttack == true)
            {
                Distance();

                move.PerseguirAlJugador();
            }
        }
    }

#region Elementos
    public void ChangeElement()
    {
        //pasar al siguiente elemnento de la lista de activeElements
    }

    public void UpdateActiveElements()
    {
        if(fire /* && no esta en activeElements el fuego*/)
        {
            //añadir al ultimo puesto de activeElemets el fuego

            //eliminar el fuego de allElements
        }

        if (water /* && no esta en activeElements el water*/)
        {
            //añadir al ultimo puesto de activeElemets el water

            //eliminar el water de allElements
        }

        if (electricity /* && no esta en activeElements el electricity*/)
        {
            //añadir al ultimo puesto de activeElemets el electricity

            //eliminar el electricity de allElements
        }

        if (rock /* && no esta en activeElements el rock*/)
        {
            //añadir al ultimo puesto de activeElemets el rock

            //eliminar el rock de allElements
        }

    }
#endregion

#region Ataques
    public void Melee()
    {
        if(MA <= 50 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("meleeAttack", 1);

            Debug.Log("Giro");
        }
        else if (MA >= 51 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("meleeAttack", 2);

            Debug.Log("Pegar");
        }
    }

    public void MeleeIdle()
    {
        bossAnim.SetInteger("meleeAttack", 0);
    }

    public void Distance()
    {
        if (DA <= 50 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("distanceAttack", 1);

            Debug.Log("Bola");
        }
        else if (DA >= 51 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("distanceAttack", 2);

            Debug.Log("Rayo");
        }
    }

    public void DsitanceIdle()
    {
        bossAnim.SetInteger("distanceAttack", 0);
    }

    private void TimeAttack()
    {
        currentTimeAttack += Time.deltaTime;
    }

    public void ResetTimeAttack()
    {
        currentTimeAttack = 0;

        MA = Random.RandomRange(0, 101);
        DA = Random.RandomRange(0, 101);

        Debug.Log(MA);
        Debug.Log(DA);
    }
#endregion

    public void BossDeath()
    {
        bossAnim.SetBool("death", true);
    }

    public void DestoryBoss()
    {
        Destroy(gameObject);
    }
    
    public void OnEnable()
    {
        UpdateActiveElements();

        currentLiife = maxLiife;
    }
}

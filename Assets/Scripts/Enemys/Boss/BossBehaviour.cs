using System.Collections.Generic;
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

    private Color rojoPersonalizado;
    private Color marronPersonalizado;
    private Color azulPersonalizado;
    private Color amarilloPersonalizado;

    public Image Vida;
    public Image Vida2;

    /*public List<string> activeElements = new List<string>();
    public List<string> allElements = new List<string>();*/
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

    private bool inAttack;

    public BossMovement move;

    private int MA;
    private int DA;
    [Space]

    [Header("MelleCollider")]
    public GameObject SwordDamage1;
    public GameObject SwordDamage2;
    [Space]

    [Header("PuntosDisparo")]
    public GameObject bola;
    public GameObject preRayo;
    public GameObject rayo;
    [Space]
    public Transform point01;
    public Transform point02;
    public Transform point03;
    public Transform point04;
    public Transform point05;
    public Transform point06;
    public Transform point07;
    public Transform point08;
    [Space]

    [Header("Muerte")]
    public bool deathBoss;

    public void Start()
    {
        ColorUtility.TryParseHtmlString("#EF4A4A", out rojoPersonalizado);
        ColorUtility.TryParseHtmlString("#B07C66", out marronPersonalizado);
        ColorUtility.TryParseHtmlString("#BDBB6C", out amarilloPersonalizado);
        ColorUtility.TryParseHtmlString("#525FEF", out azulPersonalizado);

        melee = FindAnyObjectByType<MeleeAtack>();
        distance = FindAnyObjectByType<DistanceAttack>();
        move = GetComponent<BossMovement>();

        deathBoss = false;

        Vida.color = rojoPersonalizado;
        Vida2.color = rojoPersonalizado;
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

        if(melee.meleeAtack == true && inAttack == true && !deathBoss || distance.distanceAttack == true && inAttack == true && !deathBoss)
        {
            move.Direccion();
        }
        else if(!melee.meleeAtack && !inAttack && !deathBoss|| !distance.distanceAttack && !inAttack && !deathBoss)
        {
            move.PerseguirAlJugador();
        }

        if (currentLiife <= 1950)
        {
            if (lifesCount == 0)
            {
                ChangeElement();
            }
            if (currentLiife <= 1300)
            {
                if (lifesCount == 1)
                {
                    ChangeElement();
                }
                if (currentLiife <= 650)
                {
                    if (lifesCount == 2)
                    {
                        ChangeElement();
                    }
                    if(currentLiife <= 0)
                    {
                        BossDeath();
                    }
                    else
                    {
                        Attack();
                    }
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                Attack();
            }
        }
        else
        {
            Attack();
        }
    }

#region Elementos
    public void ChangeElement()
    {
        lifesCount++;

        Debug.Log("a");

        //Cambio de color menos fuego
        if(lifesCount == 0)
        {
            Vida.color = rojoPersonalizado;
            Vida2.color = rojoPersonalizado;
        }
        else if(lifesCount == 1)
        {
            Vida.color = azulPersonalizado;
            Vida2.color = azulPersonalizado;
        }
        else if(lifesCount == 2)
        {
            Vida.color = amarilloPersonalizado;
            Vida2.color = amarilloPersonalizado;
        }
        else if (lifesCount == 3)
        {
            Vida.color = marronPersonalizado;
            Vida2.color = marronPersonalizado;
        }

    }

    #region PARA LAS MEJORAS RANDOMIZAR LOS ELEMENTOS EN QUE PELEAR
    /*public void UpdateActiveElements()
    {
        if (fire && no esta en activeElements el fuego)
        {
            //añadir al ultimo puesto de activeElemets el fuego

            //eliminar el fuego de allElements
        }

        if (water && no esta en activeElements el water)
        {
            //añadir al ultimo puesto de activeElemets el water

            //eliminar el water de allElements
        }

        if (electricity && no esta en activeElements el electricity)
        {
            //añadir al ultimo puesto de activeElemets el electricity

            //eliminar el electricity de allElements
        }

        if (rock && no esta en activeElements el rock)
        {
            //añadir al ultimo puesto de activeElemets el rock

            //eliminar el rock de allElements
        }
    }*/
    #endregion

#endregion

#region Ataques
    public void Attack()
    {
        TimeAttack();

        if (melee.meleeAtack == true && deathBoss == false)
        {
            Melee();
        }
        else if (distance.distanceAttack == true && deathBoss == false)
        {
            Distance();
        }
    }
    public void Melee()
    {
        if(MA <= 50 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("meleeAttack", 1);

            inAttack = true;
        }
        else if (MA >= 51 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("meleeAttack", 2);

            inAttack = true;
        }
    }

    public void MeleeIdle()
    {
        bossAnim.SetInteger("meleeAttack", 0);

        inAttack = false;
    }

    public void MeleeSwordDamage1()
    {
        SwordDamage1.SetActive(!SwordDamage1.activeSelf);
    }

    public void MeleeSwordDamage2()
    {
        SwordDamage2.SetActive(!SwordDamage2.activeSelf);
    }

    public void Distance()
    {
        if (DA <= 50 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("distanceAttack", 1);

            inAttack = true;
        }
        else if (DA >= 51 && currentTimeAttack >= timeAttack)
        {
            bossAnim.SetInteger("distanceAttack", 2);

            inAttack = true;
        }
    }

    public void DsitanceIdle()
    {
        bossAnim.SetInteger("distanceAttack", 0);

        inAttack = false;
    }

    public void InstanceBolas()
    {
        Instantiate(bola, point01.position, point01.rotation);
        Instantiate(bola, point02.position, point02.rotation);
        Instantiate(bola, point03.position, point03.rotation);
        Instantiate(bola, point04.position, point04.rotation);
        Instantiate(bola, point05.position, point05.rotation);
        Instantiate(bola, point06.position, point06.rotation);
        Instantiate(bola, point07.position, point07.rotation);
        Instantiate(bola, point08.position, point08.rotation);
        Debug.Log("Bolas");
    }

    public void InstancePreRayo()
    {
        preRayo.SetActive(!preRayo.activeSelf);
    }

    public void InstanceRayo()
    {
        rayo.SetActive(!rayo.activeSelf);
    }

    private void TimeAttack()
    {
        currentTimeAttack += Time.deltaTime;
    }

    public void ResetTimeAttack()
    {
        currentTimeAttack = 0;

        MA = Random.Range(0, 101);
        DA = Random.Range(0, 101);
    }
#endregion

    public void TakeDamage(float damageAmount)
    {
        if(currentLiife >= maxLiife * 0.75f && fire == true)
        {
            currentLiife -= damageAmount;
        }
        else if(currentLiife < maxLiife * 0.75f && currentLiife >= maxLiife * 0.5f && water == true)
        {
            currentLiife -= damageAmount;
        }
        else if (currentLiife < maxLiife * 0.5f && currentLiife >= maxLiife * 0.25f && electricity == true)
        {
            currentLiife -= damageAmount;
        }
        else if (currentLiife < maxLiife * 0.25f && rock == true)
        {
            currentLiife -= damageAmount;
        }
    }

    public void BossDeath()
    {
        bossAnim.SetBool("death", true);

        deathBoss = true;

        Debug.Log("Muerto");
    }

    public void DestoryBoss()
    {
        Destroy(gameObject);
    }
    
    public void OnEnable()
    {
        /*UpdateActiveElements();*/

        currentLiife = maxLiife;
        lifesCount = 0;

        rayo.SetActive(false); 
        preRayo.SetActive(false);

        //Cambio de color fuego
        Vida.color = rojoPersonalizado;
        Vida2.color = rojoPersonalizado;
    }
}

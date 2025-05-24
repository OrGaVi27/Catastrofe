using UnityEngine;

public partial class BossBehaviour : MonoBehaviour
{
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

        element = 1;
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

        //Cambio de color menos fuego
        if (lifesCount == 0)
        {
            Vida.color = rojoPersonalizado;
            Vida2.color = rojoPersonalizado;

            element = 1;

            foreach (var effect in vfx)
            {
                effect.GetComponent<Renderer>().material = materials[element];
            }
        }
        else if (lifesCount == 1)
        {
            Vida.color = azulPersonalizado;
            Vida2.color = azulPersonalizado;

            element = 4;

            foreach (var effect in vfx)
            {
                effect.GetComponent<Renderer>().material = materials[element];
            }
        }
        else if (lifesCount == 2)
        {
            Vida.color = amarilloPersonalizado;
            Vida2.color = amarilloPersonalizado;

            element = 3;

            foreach (var effect in vfx)
            {
                effect.GetComponent<Renderer>().material = materials[element];
            }
        }
        else if (lifesCount == 3)
        {
            Vida.color = marronPersonalizado;
            Vida2.color = marronPersonalizado;

            element = 2;

            foreach (var effect in vfx)
            {
                effect.GetComponent<Renderer>().material = materials[element];
            }
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

#region Damage
    public void TakeDamage(float damageAmount, int attackElement, float elementMultiplier)
    {
        if (currentLiife >= maxLiife * 0.75f && fire == true)
        {
            if (attackElement == 0)
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 1 && element == 4)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 4 && element == 1)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else if (attackElement - 1 == element)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement + 1 == element)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
        }
        else if (currentLiife < maxLiife * 0.75f && currentLiife >= maxLiife * 0.5f && water == true)
        {
            if (attackElement == 0)
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 1 && element == 4)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 4 && element == 1)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else if (attackElement - 1 == element)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement + 1 == element)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
        }
        else if (currentLiife < maxLiife * 0.5f && currentLiife >= maxLiife * 0.25f && electricity == true)
        {
            if (attackElement == 0)
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 1 && element == 4)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 4 && element == 1)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else if (attackElement - 1 == element)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement + 1 == element)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
        }
        else if (currentLiife < maxLiife * 0.25f && rock == true)
        {
            if (attackElement == 0)
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 1 && element == 4)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement == 4 && element == 1)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else if (attackElement - 1 == element)
            {
                currentLiife -= damageAmount * 0.5f;
                bossAnim.SetTrigger("Hit");
            }
            else if (attackElement + 1 == element)
            {
                currentLiife -= damageAmount * elementMultiplier;
                bossAnim.SetTrigger("BigHit");
            }
            else
            {
                currentLiife -= damageAmount;
                bossAnim.SetTrigger("Hit");
            }
        }
    }
#endregion

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

    public void StartBattle()
    {
        lifesCount = 0;
        currentLiife = maxLiife;

        element = 1;

        rayo.SetActive(false);
        preRayo.SetActive(false);

        Vida.color = rojoPersonalizado;
        Vida2.color = rojoPersonalizado;

        element = 1;

        foreach (var effect in vfx)
        {
            effect.GetComponent<Renderer>().material = materials[element];
        }
    }

    public void OnEnable()
    {
        StartBattle();
    }
}

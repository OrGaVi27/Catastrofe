using Unity.VisualScripting;
using UnityEngine;

public class FlechaPlayer : MonoBehaviour
{
    public GameObject player;
    public float tiempoDeVidaTotal;
    public float velocidadFlecha;
    public bool flechaEspecial = false;
    public GameObject ExplosionPrefab;
    public Material[] materials;
    public GameObject[] Flechas;

    public float damage;
    private float currentDeleteTime = 0f;
    private Rigidbody rb;
    private int hits = 0;

    int playerElement;
    float elementMult;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentDeleteTime = 0;


        damage = player.GetComponent<PlayerStateManager>().DamageOutput();

        playerElement = player.GetComponent<PlayerStateManager>().element;
        elementMult = player.GetComponent<BasePlayerStats>().elementMultiplier;

        flechaEspecial = player.GetComponent<PlayerStateManager>().anim.GetBool("StrongAttack");

        rb = GetComponent<Rigidbody>();

        ElementChange();

    }

    void Update()
    {
        currentDeleteTime += Time.deltaTime;

        if (currentDeleteTime >= tiempoDeVidaTotal)
        {
            Destroy(gameObject);
        }

        //Velocidad hacia el jugador
        rb.velocity = transform.forward * velocidadFlecha;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaseEnemyStats>() != null || other.tag == "Ground" || other.tag == "Wall")
        {
            if (other.gameObject.GetComponent<BaseEnemyStats>() != null && hits == 0)
            {
                hits++;
                if (flechaEspecial)
                {
                    GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); // Instancia la explosion Elemental

                    DamageTrigger dt = explosion.GetComponent<DamageTrigger>();
                    if (dt != null) dt.SetDamage(damage, playerElement, elementMult);


                    Explosion boom = explosion.GetComponent<Explosion>();
                    if (boom != null) boom.SetElement(playerElement);
                }
                else
                {
                    other.gameObject.GetComponent<BaseEnemyStats>().TakeDamage(damage, playerElement, elementMult); // Llama a la función de daño del enemigo
                }
                
                if (player.GetComponent<BasePlayerStats>().currentMana < player.GetComponent<BasePlayerStats>().maxMana && player.GetComponent<PlayerStateManager>().element == 0)
                {
                    player.GetComponent<BasePlayerStats>().currentMana++;
                }

            }


            Destroy(gameObject);
        }

    }

    void ElementChange()
    {
        switch (playerElement)
            {
                case 0:
                    Flechas[0].GetComponent<MeshRenderer>().material = materials[0];
                    Flechas[1].GetComponent<MeshRenderer>().material = materials[0];
                    break;

                case 1:
                    Flechas[0].GetComponent<MeshRenderer>().material = materials[1];
                    Flechas[1].GetComponent<MeshRenderer>().material = materials[1];
                    break;

                case 2:
                    Flechas[0].GetComponent<MeshRenderer>().material = materials[2];
                    Flechas[1].GetComponent<MeshRenderer>().material = materials[2];
                    break;

                case 3:
                    Flechas[0].GetComponent<MeshRenderer>().material = materials[3];
                    Flechas[1].GetComponent<MeshRenderer>().material = materials[3];
                    break;

                case 4:
                    Flechas[0].GetComponent<MeshRenderer>().material = materials[4];
                    Flechas[1].GetComponent<MeshRenderer>().material = materials[4];
                    break;
            }
    }
}

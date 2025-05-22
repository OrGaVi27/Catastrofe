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
    private float currentDeleteTime;
    private Vector3 direccion;
    private Rigidbody rb;

    int playerElement;
    float elementMult;


    void Start()
    {
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
        rb.velocity = direccion * velocidadFlecha;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseEnemyStats>() != null || other.tag == "Ground" || other.tag == "Wall")
        {
            if (other.GetComponent<BaseEnemyStats>() != null)
            {
                if (flechaEspecial)
                {
                    GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); // Instancia la explosion Elemental
                    explosion.GetComponent<DamageTrigger>().SetDamage(damage, playerElement, elementMult);
                    explosion.GetComponent<Explosion>().SetElement(playerElement);
                }
                else
                {
                    other.GetComponent<BaseEnemyStats>().TakeDamage(damage, playerElement, elementMult); // Llama a la función de daño del enemigo
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

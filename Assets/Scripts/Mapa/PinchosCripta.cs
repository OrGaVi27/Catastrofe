using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosCripta : MonoBehaviour
{
    public BaseEnemyStats[] enemigos;
    public Collider bloqueo;
    int enemigosVivos;
    private bool eventoEjecutado = false;
    [SerializeField] private AudioClip Pinxos;



    // Start is called before the first frame update
    void Start()
    {
        enemigosVivos = enemigos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventoEjecutado) return;

        bool todosMuertos = true;
        foreach (BaseEnemyStats enemigo in enemigos)
        {
            if (enemigo != null && enemigo.currentHealth > 0)
            {
                todosMuertos = false;
                break;
            }
        }

        if (todosMuertos)
        {
            GetComponent<Animator>().SetBool("AllDeath", true);
            ControladorSonido.Instance.EjecutarSonido(Pinxos);
            bloqueo.enabled = false;
            eventoEjecutado = true;
        }
    }
}

using UnityEngine;

public class SpawnPointsPlayer : MonoBehaviour
{
    public GameObject player;

    public Transform spawn;
    public GameObject habitacionSpawn;
    public GameObject habitacionActual;
    [Space]
    public GameObject guardado;

    void Start()
    {
        transform.position = guardado.GetComponent<GuardarPartida>().datosGuardado.spawnPosition;
    }

    void Update()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (player.GetComponent<BasePlayerStats>().currentHealth <= 0f)
        {
            if (habitacionActual != habitacionSpawn)
            {
                habitacionActual.SetActive(false);
                habitacionSpawn.SetActive(true);
            }

            transform.position = spawn.position;
            Physics.SyncTransforms();
        }
    }
}

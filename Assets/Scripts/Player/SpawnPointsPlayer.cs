using UnityEngine;

public class SpawnPointsPlayer : MonoBehaviour
{
    public GameObject player;

    public Transform spawn;

    void Start()
    {
        //spawn.position = spawnGuardado.transform.position;

        //Quitar cuando exista guardado
        spawn.position = transform.position;
        
        Debug.Log(spawn.position);
    }

    void Update()
    {
        //Spawn();
    }

    public void Spawn()
    {
        if (player.GetComponent<BasePlayerStats>().currentHealth <= 0f)
        {
            // Desactivar CharacterController
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            transform.position = spawn.position;

            // Volver a activar CharacterController
            if (cc != null) cc.enabled = true;
        }
    }
}

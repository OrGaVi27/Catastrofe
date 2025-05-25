using UnityEngine;

public class SpawnPointsPlayer : MonoBehaviour
{
    public GameObject player;

    public Transform spawn;

    void Start()
    {
        //spawn.position = spawnGuardado.transform.position;
        
        Debug.Log(spawn.position);
    }

    void Update()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (player.GetComponent<BasePlayerStats>().currentHealth <= 0f)
        {
            transform.position = spawn.position;
            Physics.SyncTransforms();
        }
    }
}

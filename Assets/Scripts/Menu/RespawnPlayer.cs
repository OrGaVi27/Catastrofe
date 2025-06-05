using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Respawn()
    {
        player.GetComponent<SpawnPointsPlayer>().Spawn();
        player.GetComponent<PlayerStateManager>().anim.SetBool("Death", false);
    }
}

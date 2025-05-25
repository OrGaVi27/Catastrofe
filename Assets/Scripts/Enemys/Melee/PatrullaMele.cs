using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrullaMele : MonoBehaviour
{

    //patrullaDos
    public GameObject mesh;
    public float wanderRadius;


    //..
    private NavMeshAgent enemigoMele;

    private float timer;
    public float timeGuard = 5f;
    public Transform[] waypoint;
    private int indiceWaypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        enemigoMele = this.GetComponent<NavMeshAgent>();
        enemigoMele.SetDestination(waypoint[indiceWaypoint].position);
        
        mesh = GameObject.Find("plane");
        enemigoMele = GetComponent<NavMeshAgent>();
        timer = timeGuard;

    }

    // Update is called once per frame
     void Update()
    {
        Patrullaje();
    }

    void Patrullaje()
    {
        Debug.Log("Patrullando");
        if (!enemigoMele.pathPending && enemigoMele.remainingDistance <= 0.5f)
        {
            timer += Time.deltaTime; 

            if (timer >= timeGuard)
            {
                timer = 0f; 
                indiceWaypoint = (indiceWaypoint + 1) % waypoint.Length; 
                enemigoMele.SetDestination(waypoint[indiceWaypoint].position); 
            }
        }
        else
        {
            
            enemigoMele.SetDestination(waypoint[indiceWaypoint].position);
        }
        
    }
    void perseguir()
    {

    }
    void patrullaDos()
    {
        // timer += Time.deltaTime;
        // if (timer >= timeGuard)
        // {
        //     Vector3 newPos = RandomNavSphere(mesh.transform.position, wanderRadius, -1);
        //     enemigoMele.SetDestination(newPos);
        //     enemigoMele.speed = Random.Range(1.0f, 4.0f);
        //     timer = 0f;
        // }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class MoverClick : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public NavMeshAgent agent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Mover();
        }
    }

    void Mover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}

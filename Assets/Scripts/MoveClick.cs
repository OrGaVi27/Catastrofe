using UnityEngine;
using UnityEngine.AI;

public class MoveClick : MonoBehaviour
{

    public Camera mainCamera;
    public NavMeshAgent agent;


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }
    void Move(){
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
        
    }
}

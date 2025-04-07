using UnityEngine;
using UnityEngine.AI;

public class SeguirPlayer : MonoBehaviour
{

    public Transform Target;
    public float DiscanciaAtaque;

    private NavMeshAgent m_Agent;
    // private Animator m_Animator;
    private float m_Distance;

    void Start()
    {
        
        m_Agent = GetComponent<NavMeshAgent>();
        // m_Animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);
        if (m_Distance < DiscanciaAtaque)
        {
            m_Agent.isStopped = true;
            // m_Animator.SetBool("Attack", true);
            Debug.Log("Ataque");
        }
        else{
            m_Agent.isStopped = false;
            // m_Animator.SetBool("Attack", false);
            m_Agent.SetDestination(Target.position);
        }
    }
}

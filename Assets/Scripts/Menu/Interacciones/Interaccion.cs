using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Interaccion : MonoBehaviour
{

    public GameObject canvas;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        animator.SetBool("Out", false);

    }

    void OnTriggerExit(Collider other)
    {
        animator.SetBool("Out", true);

    }

    public void DesactivarCanvas()
    {
        canvas.SetActive(false);

    }
}

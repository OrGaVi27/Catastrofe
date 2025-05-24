using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public GameObject explosionPrefab;
    private float explosionTime = 1f;
    private float explosionStartTime;

    void Start()
    {
        explosionStartTime = Time.time;
    }

    void Update()
    {
        if (explosionStartTime + explosionTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    public void SetElement(int element)
    {
        var colorMult = 0.02f;
        switch (element)
        {
            case 0:
                explosionPrefab.GetComponent<Renderer>().material.color = new Color(191, 191, 191, 0) * colorMult;
                break;

            case 1:
                explosionPrefab.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0) * colorMult;
                break;

            case 2:
                explosionPrefab.GetComponent<Renderer>().material.color = new Color(214, 69, 12, 0) * colorMult;
                break;

            case 3:
                explosionPrefab.GetComponent<Renderer>().material.color = new Color(255, 251, 0, 0) * colorMult;
                break;

            case 4:
                explosionPrefab.GetComponent<Renderer>().material.color = new Color(0, 21, 255, 0) * colorMult;
                break;
        }

    }
}

using UnityEngine;

public class PageFloat : MonoBehaviour
{
    public float amplitude = 0.05f; // altura del movimiento
    public float frequency = 1f;    // velocidad del movimiento

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = amplitude * Mathf.Sin(Time.time * frequency * 2 * Mathf.PI);
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
}

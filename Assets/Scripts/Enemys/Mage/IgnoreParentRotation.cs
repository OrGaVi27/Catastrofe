using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour
{
    GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
    }
}

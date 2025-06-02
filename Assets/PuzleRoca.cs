using UnityEngine;

public class PuzleRoca : PuzleActivator
{
    public float rotatingSpeed = 1.0f;
    [Range(-1, 1)]
    public int rotateDirection = -1;
    private Quaternion targetRotation;
    private Vector3 baseEulerAngles;

    void Start()
    {
        targetRotation = transform.localRotation;
        baseEulerAngles = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if(transform.localRotation != targetRotation) wasActivated = false;

        if (wasActivated)
        {
            wasActivated = false;
            if (GetAttackElement() == 2)
            {
                baseEulerAngles += new Vector3(0, 90 * rotateDirection, 0); // Always add to the base
                targetRotation = Quaternion.Euler(baseEulerAngles);
            }
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * rotatingSpeed);
        }
    }


}

using UnityEngine;

public class PuzleActivator : MonoBehaviour
{
    
    public bool wasActivated = false;
    int attackElement;


    public void SetAttackElement(int element)
    {
        attackElement = element;
    }

    
    public int GetAttackElement()
    {
        return attackElement;
    }
}

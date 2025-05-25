
using UnityEngine;

public class PuzleElectrico : PuzleActivator
{
    public int id;

    void Update()
    {
        if (wasActivated)
        {
            wasActivated = false;
            if (GetAttackElement() == 3)
            {
                Debug.Log("Activando plataforma con id: " + id);
                //Activar plataformas con el mismo id
                GameEvents.current.SwitchTrigger(id);
            }
        }
    }


}

using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Events
    //Palancas que activan las plataformas
    public event Action<int> onSwitchTrigger;

    public void SwitchTrigger(int id)
    {
        if (onSwitchTrigger != null)
        {
            onSwitchTrigger(id);
        }
    }

    //Fade para cambiar de habitación
    public event Action onRoomFadeTrigger;

    public void RoomFadeTrigger()
    {
        if (onRoomFadeTrigger != null)
        {
            onRoomFadeTrigger();
        }
    }

    public event Action onInteract;
    
    public void Interact()
    {
        if (onInteract != null)
        {
            onInteract();
        }
    }
    #endregion
}

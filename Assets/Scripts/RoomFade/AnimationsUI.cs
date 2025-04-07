using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsUI : MonoBehaviour
{
    Animator roomFadeAnimator;

    void Start()
    {
        roomFadeAnimator = GetComponent<Animator>();
        GameEvents.current.onRoomFadeTrigger += OnRoomFade;
    }


    private void OnRoomFade()
    {
        roomFadeAnimator.SetTrigger("RoomFade");
    }
}

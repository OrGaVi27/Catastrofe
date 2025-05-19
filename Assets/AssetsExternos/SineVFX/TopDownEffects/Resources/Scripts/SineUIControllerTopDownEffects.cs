using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SineUIControllerTopDownEffects : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public PrefabSpawner prefabSpawnerObject;
    public Text nameInUI;

    private string nameOfThePrafab;

    private void Start()
    {
        //
    }

    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            canvasGroup.alpha = 1f - canvasGroup.alpha;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            ChangeEffect(true);
        }
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            ChangeEffect(false);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            prefabSpawnerObject.SpawnPrefab();
        }

        nameOfThePrafab = prefabSpawnerObject.nameOfThePrefab;
        nameInUI.text = "Spawn - " + nameOfThePrafab;
    }

    // Change active VFX
    public void ChangeEffect(bool bo)
    {
        prefabSpawnerObject.ChangePrefabIntex(bo);
        nameOfThePrafab = prefabSpawnerObject.nameOfThePrefab;
    }
}

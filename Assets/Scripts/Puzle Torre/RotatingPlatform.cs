using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    public int id;

    public float maxRotation = 90f;
    public float rotationSpeed = 65f;
    public float rotationDirection = 1f;

    private bool isRotating = false;
    private float currentRotation;


    private void Start()
    {
        currentRotation = maxRotation;
        GameEvents.current.onSwitchTrigger += OnPlatformRotate;
    }

    public void OnPlatformRotate(int id)
    {
        if (id == this.id)
        {
            if (!isRotating)
            {
                isRotating = true;
                currentRotation = 0;
            }
        }
    }

    private void RotateToPosition()
    {
        if (currentRotation < maxRotation)
        {
            transform.Rotate(0, rotationDirection * rotationSpeed * Time.deltaTime, 0);
            currentRotation += rotationSpeed * Time.deltaTime;
        }
        else
        {
            isRotating = false;
        }
    }

    private void Update()
    {
        RotateToPosition();
    }
}

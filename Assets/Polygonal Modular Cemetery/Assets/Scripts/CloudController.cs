using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonalCemetery
{
    public class CloudController : MonoBehaviour
    {
        public float speed = 1f;
        public float distance = 10f;

        bool changeDirection = true;
        bool isMovingRight = true;
        // Update is called once per frame
        void Update()
        {
            if (changeDirection)
            {
                StartCoroutine("MoveClouds");
            }
        }
        IEnumerator MoveClouds()
        {
            changeDirection = false;
            float movingDelta = 0f;
            float distnceCounter = 0f;
            float directionFactor = isMovingRight ? 1f : -1f;
            while (distnceCounter < distance)
            {
                movingDelta = Time.deltaTime * speed;
                transform.Translate(Vector3.right * movingDelta * directionFactor);
                distnceCounter += movingDelta;
                yield return null;
            }
            isMovingRight = !isMovingRight;
            changeDirection = true;
        }
    }
}

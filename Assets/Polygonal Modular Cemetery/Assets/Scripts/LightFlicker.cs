using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonalCemetery
{
    public class LightFlicker : MonoBehaviour
    {
        Light lc;
        bool changeRange = true;
        bool isRangeUp = false;
        // Start is called before the first frame update
        void Start()
        {
            lc = GetComponent<Light>();

        }
        private void Update()
        {
            if (lc != null)
            {
                if (changeRange)
                {
                    if (isRangeUp)
                    {
                        StartCoroutine("LightUp");
                    }
                    else
                    {
                        StartCoroutine("LightDown");
                    }
                }
            }
        }
        IEnumerator LightUp()
        {
            changeRange = false;
            while (lc.range < 10)
            {
                lc.range += 8 * Time.deltaTime;
                yield return null;
            }
            isRangeUp = false;
            changeRange = true;
        }
        IEnumerator LightDown()
        {
            changeRange = false;
            while (lc.range > 9.5f)
            {
                lc.range -= 8 * Time.deltaTime;
                yield return null;
            }
            isRangeUp = true;
            changeRange = true;
        }
    }
}

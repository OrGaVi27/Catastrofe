using Unity.VisualScripting;
using UnityEngine;

public class AnimLayerControl : MonoBehaviour
{
    public Animator anim;
    public int layerIndex;
    public float layerWeightChangeSpeed = 1f;
    private float finalWeight = 1f;

    public void SetLayerWeightMax()
    {
        anim.SetLayerWeight(layerIndex, 1);
        finalWeight = 1;
    }

    public void SetLayerWeightMin()
    {
        if (finalWeight - Time.deltaTime * layerWeightChangeSpeed > 0.01f)
        {
            finalWeight -= Time.deltaTime * layerWeightChangeSpeed;
        }
        else
        {
            finalWeight = 0.01f;
        }
        anim.SetLayerWeight(layerIndex, finalWeight);
    }
}

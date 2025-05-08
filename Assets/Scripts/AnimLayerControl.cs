using UnityEngine;

public class AnimLayerControl : MonoBehaviour
{
    public Animator anim;
    public int layerIndex;

    public void SetLayerWeightMax()
    {
        anim.SetLayerWeight(layerIndex, 1);
    }

    public void SetLayerWeightMin()
    {
        anim.SetLayerWeight(layerIndex, 0.01f);
    }
}

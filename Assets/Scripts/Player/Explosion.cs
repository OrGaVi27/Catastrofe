using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public GameObject explosionPrefab;
    public void SetElement(int element)
    {
        var baseNum = 255;
        switch (element)
        {
            case 0:
                var main = explosionPrefab.GetComponent<ParticleSystem>().main;
                main.startColor = new Color(255 /baseNum, 255/baseNum, 255/baseNum, 30/baseNum);
                break;

            case 1:
                var main1 = explosionPrefab.GetComponent<ParticleSystem>().main;
                main1.startColor = new Color(255/baseNum, 0, 0, 5/baseNum);
                break;

            case 2:
                var main2 = explosionPrefab.GetComponent<ParticleSystem>().main;
                main2.startColor = new Color(214/baseNum, 69/baseNum, 12/baseNum, 5/baseNum);
                break;

            case 3:
                var main3 = explosionPrefab.GetComponent<ParticleSystem>().main;
                main3.startColor = new Color(255/baseNum, 251/baseNum, 0, 5/baseNum);
                break;

            case 4:
                var main4 = explosionPrefab.GetComponent<ParticleSystem>().main;
                main4.startColor = new Color(0, 21/baseNum, 255/baseNum, 5/baseNum);
                break;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiaElemento : MonoBehaviour
{
    public int elemento = 0;
    public Sprite[] elementoSprite; // Array de sprites para los diferentes elementos

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            elemento = 0;
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            elemento = 1;  
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            elemento = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            elemento = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            elemento = 4;
        } 

        switch (elemento)
        {
            case 0:
                gameObject.GetComponent<Image>().sprite = elementoSprite[0];
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = elementoSprite[1];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = elementoSprite[2];
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = elementoSprite[3];
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = elementoSprite[4];
                break;

        }
    }
}

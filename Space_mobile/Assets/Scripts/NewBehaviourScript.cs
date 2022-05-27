using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public bool building;

    void OnMouseEnter()
    {
        if (building == true)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(50/255.0f, 200/255.0f, 50/255.0f, 150/255.0f);
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(200/255.0f, 50/255.0f, 50/255.0f, 150/255.0f);
        }
    }

    void OnMouseExit()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(100/255.0f, 100/255.0f, 100/255.0f, 200/255.0f);
    }
}
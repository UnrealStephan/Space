using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.03f);
    }

    void OnMouseExit()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
    }
}

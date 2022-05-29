using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{

    public GameObject shopPanel;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (shopPanel.active)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shopPanel.SetActive(true);
        }
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

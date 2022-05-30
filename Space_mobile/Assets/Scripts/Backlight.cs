using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{

    public New NW;
    public GameObject ShopCell;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        NW.Map.SetActive(false);
        transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
        ShopCell.GetComponentInChildren<Outline>().effectColor = Color.black;
    }

    void OnMouseEnter()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.03f);
    }

    void OnMouseExit()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
    }
}

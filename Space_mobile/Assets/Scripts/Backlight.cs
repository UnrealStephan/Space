using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{

    public bool Busy = false;
    public GameObject Shop;
    public GameObject Map;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (Busy == true)
        {
            return;
        }
        else
        {
            Instantiate(Shop.transform.GetComponent<New_spaceship>().Active_ship).transform.position = transform.GetChild(5).transform.position;
            Busy = true;
            Map.SetActive(false);
            Shop.transform.GetComponent<New_spaceship>().Outline_removal();
        }
    }
}
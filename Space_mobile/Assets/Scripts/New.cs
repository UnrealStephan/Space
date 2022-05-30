using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New : MonoBehaviour
{

    public GameObject Map;
    public Backlight BL;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        BL.ShopCell = gameObject;
        transform.GetChild(0).GetComponent<Outline>().effectColor = Color.green;
        Map.SetActive(true);
    }
}
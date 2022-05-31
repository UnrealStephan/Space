using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool building = false;
    public GameObject building_obj;
    private GameObject obj;

    void OnMouseDown()
    {
        if (building == false)
        {
            obj = Instantiate(building_obj, transform.position, Quaternion.Euler(new Vector3(0, 90, 90)));
            obj.GetComponent<Entity>().cell = gameObject;
            building = true;
            transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
        }
    }

    void OnMouseEnter()
    {
        if (building == false)
        {transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.03f);}
    }

    void OnMouseExit()
    {
        if (building == false)
        {transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);}
    }
}

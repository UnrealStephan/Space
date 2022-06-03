using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{
    private GameObject obj;

    public bool Busy = false;
    public GameObject Shop;
    public GameObject Map;
    private GameObject defeat;

    void Start()
    {
        defeat = GameObject.Find("Canvas");
    }

    private void OnMouseDown()
    {
        if (Busy == true)
        {

            return;
        }
        else
        {
            if (Shop.transform.GetComponent<New_spaceship>().Active_ship.GetComponent<Entity>().price <= defeat.GetComponent<Defeat>().money)
            {
                defeat.GetComponent<Defeat>().money -= Shop.transform.GetComponent<New_spaceship>().Active_ship.GetComponent<Entity>().price;
                obj = Instantiate(Shop.transform.GetComponent<New_spaceship>().Active_ship, transform.GetChild(4).transform.position, Shop.transform.GetComponent<New_spaceship>().Active_ship.transform.rotation);
                Busy = true;
                Map.SetActive(false);
                obj.GetComponent<Entity>().cell = gameObject;
                Shop.transform.GetComponent<New_spaceship>().Outline_removal();
            }
            else
            {
                //   Ты лох и бомжара. Пока нормальные люди зарабатывают и добиваются целей,
                // ты мечтаешь о роскошном космическом кораблике. Хрен тебе! Иди работай на завод, а не в облаках летай!
            }
        }
    }
}
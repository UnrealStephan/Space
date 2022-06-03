using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool building = false;
    public GameObject building_obj;
    private GameObject obj;
    private int money;

    void OnMouseDown()
    {
        if (building == false)
        {
            if (building_obj.GetComponent<Entity>().price >= money)
            {
                // -price из баланса
                obj = Instantiate(building_obj, transform.position, building_obj.transform.rotation);
                obj.GetComponent<Entity>().cell = gameObject;
                transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
            }
            else
            {
                //   Ты лох и бомжара. Пока нормальные люди зарабатывают и добиваются целей,
                // ты мечтаешь о роскошном космическом кораблике. Хуй тебе! Иди работай на завод, а не в облаках летай!
            }
        }
    }
}

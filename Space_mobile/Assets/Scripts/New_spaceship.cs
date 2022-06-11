using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_spaceship : MonoBehaviour
{

    [Header("Models")]
    public GameObject Spaceship_1;
    public GameObject Spaceship_2;
    public GameObject Spaceship_3;
    public GameObject Spaceship_4;
    public GameObject Spaceship_5;
    [Header("Buttons")]
    public GameObject Button_1;
    public GameObject Button_2;
    public GameObject Button_3;
    public GameObject Button_4;
    public GameObject Button_5;
    [Header("Other")]
    public GameObject Map;
    public GameObject Active_ship;

    void Start()
    {

    }

    void Update()
    {

    }
    public void Outline_removal()
    {
        Button_1.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
        Button_2.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
        Button_3.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
        Button_4.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
        Button_5.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
    }

    public void Push_Button_1()
    {
        if (Button_1.transform.GetChild(0).GetComponent<Outline>().effectColor == new Color (0, 255, 0, 0.0f))
        {
            Outline_removal();
            Button_1.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 1.0f);
            Map.SetActive(true);
            Active_ship = Spaceship_1;
        }
        else
        {
            Button_1.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
            Map.SetActive(false);
        }
    }

    public void Push_Button_2()
    {
        if (Button_2.transform.GetChild(0).GetComponent<Outline>().effectColor == new Color(0, 255, 0, 0.0f))
        {
            Outline_removal();
            Button_2.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 1.0f);
            Map.SetActive(true);
            Active_ship = Spaceship_2;
        }
        else
        {
            Button_2.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
            Map.SetActive(false);
        }
    }
    public void Push_Button_3()
    {
        if (Button_3.transform.GetChild(0).GetComponent<Outline>().effectColor == new Color(0, 255, 0, 0.0f))
        {
            Outline_removal();
            Button_3.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 1.0f);
            Map.SetActive(true);
            Active_ship = Spaceship_3;
        }
        else
        {
            Button_3.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
            Map.SetActive(false);
        }
    }
    public void Push_Button_4()
    {
        if (Button_4.transform.GetChild(0).GetComponent<Outline>().effectColor == new Color(0, 255, 0, 0.0f))
        {
            Outline_removal();
            Button_4.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 1.0f);
            Map.SetActive(true);
            Active_ship = Spaceship_4;
        }
        else
        {
            Button_4.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
            Map.SetActive(false);
        }
    }
    public void Push_Button_5()
    {
        if (Button_5.transform.GetChild(0).GetComponent<Outline>().effectColor == new Color(0, 255, 0, 0.0f))
        {
            Outline_removal();
            Button_5.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 1.0f);
            Map.SetActive(true);
            Active_ship = Spaceship_5;
        }
        else
        {
            Button_5.transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(0, 255, 0, 0.0f);
            Map.SetActive(false);
        }
    }
}
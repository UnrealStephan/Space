using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    public int lives;
    public int money;
    public int waves;
    public GameObject Text_1;
    public GameObject Text_2;
    public GameObject Text_3;

    void Start()
    {
        waves = 1;
    }

    void Update()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        Text_1.transform.GetComponent<Text>().text = lives.ToString();
        Text_2.transform.GetComponent<Text>().text = money.ToString();
        Text_3.transform.GetComponent<Text>().text = waves.ToString();
    }
}

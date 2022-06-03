using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    public int lives;
    public int money;
    public int score;
    public GameObject Text_1;
    public GameObject Text_2;
    public GameObject Text_3;

    void Start()
    {
        lives = 3;
        money = 100;
        score = 0;
    }

    void Update()
    {
        if (lives <= 0)
        {
            Debug.Log("Hui chlen zhopa aboba zalupa");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        Text_1.transform.GetComponent<Text>().text = lives.ToString();
        Text_2.transform.GetComponent<Text>().text = money.ToString();
        Text_3.transform.GetComponent<Text>().text = score.ToString();
    }
}

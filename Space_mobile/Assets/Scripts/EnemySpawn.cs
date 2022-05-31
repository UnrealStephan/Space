using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemies;
    private GameObject enemy;
    public int number;
    public double spawnFrequency;
    private double time;
    private int position;

    private bool end = true;

    void Start()
    {
        time = spawnFrequency;
    }

    void Update()
    {
        if (number > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = spawnFrequency;
                position = Random.Range(0, 5);
                enemy = Instantiate(enemies, new Vector3(1000, 50 + position * 100, 0), Quaternion.Euler(new Vector3(0, -90, 90)));
                enemy.GetComponent<Entity>().type = "Enemy";
                number--;
            }
        }
        else
        {
            if (end == false)
            {
                Debug.Log("End");
                end = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies; // враги
    private GameObject enemy; // размещаемый враг (вспомог.)
    public double min_spawnFrequency; // минимальная частота спавна (конечная)
    public double max_spawnFrequency; // максимальная частота спавна (начальная)
    private double spawnFrequency; // нынешняя частота (вспомог.)
    public int compl_duration; // количество усложнений
    public int enemies_number; // количество врагов до передышки
    public int incr_waiting_resp; // увеличение врагов за каждую волну
    public int respite_time; // время передышки
    private int enemies_before_respite; // осталось врагов до передышки
    private double time; // время до спавна
    private double position; // позиция спавна врага
    public int unlocked_enemies; // разблокированные враги (с начала списка)

    public GameObject cell1;
    public GameObject cell2;

    void Start()
    {
        spawnFrequency = max_spawnFrequency;
        enemies_before_respite = enemies_number;
        time = spawnFrequency;
    }

    void Update()
    {
        if (enemies_before_respite <= 0)
        {
            if (unlocked_enemies < enemies.Length)
            {unlocked_enemies += 1;}

            time = respite_time;
            enemies_number += incr_waiting_resp;
            enemies_before_respite = enemies_number;
        }
        else
        {
            if (time <= 0)
            {
                spawnFrequency -= ((max_spawnFrequency - min_spawnFrequency) / compl_duration);
                time = spawnFrequency;
                position = cell1.transform.GetChild(4).transform.position.y - Random.Range(0, 5) * (cell1.transform.GetChild(4).transform.position.y - cell2.transform.GetChild(4).transform.position.y);
                enemy = Instantiate(enemies[Random.Range(0, unlocked_enemies)], new Vector3(100, (float)position, (float)98.4851), Quaternion.Euler(new Vector3(90, 0, 180)));
                enemy.GetComponent<Entity>().type = "Enemy";
                enemies_before_respite--;
            }
        }

        time -= Time.deltaTime;
    }
}

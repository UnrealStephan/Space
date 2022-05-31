using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Обязательные переменные
    public string type;
    public Mesh model;
    public Material material;
    public int hp;
    public int price;
    public int speed;

    // Дальний бой
    public bool shooting;
    public GameObject bullet;
    private GameObject fired_bullet;
    public Mesh bullet_model;
    public Material bullet_material;
    public int bullet_damage;
    public int bullet_speed;
    public int bullet_boost;
    public int shooting_range;
    public double shooting_recharge;
    private double recharging;
    public bool objDetected;

    // Ближний бой
    public bool beating;
    public int b_damage;
    public int b_range;
    public int b_recharging;

    // Взрыв снаряда
    public bool explosion;
    public int e_radius;
    public int e_damage;

    // Самоубийство при соприкосновении с постройкой
    public bool kamikadze;
    public int k_damage;

    // Взрыв при самоубийстве
    public bool k_explosion;
    public int k_e_radius;
    public int k_e_damage;

    public GameObject cell;

    void Start()
    {
        recharging = shooting_recharge;
        GetComponent<MeshFilter>().mesh = model;
        GetComponent<MeshRenderer>().material = material;
    }

    void Update()
    {
        // Смэрть
        if (hp <= 0)
        {
            if (type == "Building")
            {cell.GetComponent<Cell>().building = false;}

            Destroy(gameObject);
        }

        // Перемещение объекта
        if (speed != 0)
        {transform.Translate(0, 0, speed * Time.deltaTime);}

        // Дальний бой
        if (shooting)
        {
            if (objDetected)
            {
                if (recharging <= 0)
                {
                    Debug.Log("Piu!");
                    if (type == "Building")
                    {fired_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 90, 90)));}
                    if (type == "Enemy")
                    {fired_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, -90, 90)));}

                    fired_bullet.GetComponent<MeshFilter>().mesh = bullet_model;
                    fired_bullet.GetComponent<Bullet>().sender = type;
                    fired_bullet.GetComponent<Bullet>().damage = bullet_damage;
                    fired_bullet.GetComponent<Bullet>().speed = bullet_speed;
                    fired_bullet.GetComponent<Bullet>().boost = bullet_boost;
                    fired_bullet.GetComponent<Bullet>().explosion = explosion;
                    fired_bullet.GetComponent<Bullet>().expl_radius = e_radius;
                    fired_bullet.GetComponent<Bullet>().expl_damage = e_damage;

                    recharging = shooting_recharge;
                }

                recharging -= Time.deltaTime;
            }
        }

        // Ближний бой
        if (beating)
        {

        }
    }

    // Действие при соприкосновении объекта с другим объектом
    void OnCollisionEnter(Collision col)
    {
        if (kamikadze)
        {
            if (type == "Building")
            {
                if (col.gameObject.GetComponent<Entity>().type == "Enemy")
                {
                    col.gameObject.GetComponent<Entity>().hp -= k_damage;
                    if (k_explosion)
                    {
                        // бум
                    }
                    Destroy(gameObject);
                }
            }

            if (type == "Enemy")
            {
                if (col.gameObject.GetComponent<Entity>().type == "Building")
                {
                    col.gameObject.GetComponent<Entity>().hp -= k_damage;
                    if (k_explosion)
                    {
                        // бум
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
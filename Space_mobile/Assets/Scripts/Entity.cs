using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Общие переменные
    public string type;
    public Mesh model;
    public Material material;
    public int hp;
    public int price;
    public int speed;

    // Дальний бой
    public bool shooting;
    public Mesh bullet_model;
    public Material bullet_material;
    public int bullet_damage;
    public float bullet_speed;
    public float bullet_boost;
    public int shooting_range; // пока не готово
    public double shooting_rate;
    public double shooting_queue;
    public double shooting_recharge;

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

    // Вспомогательные переменные (в эдиторе трогать не надо!)
    public GameObject cell;
    public GameObject bullet;
    private GameObject fired_bullet;
    public GameObject e_prefab;
    private GameObject expl_obj;
    private Ray sr_ray;
    private RaycastHit hit;
    private Vector3 ray_rotation;
    private double recharging;
    private double sh_delay;
    private double sh_queue;
    private int target = 1 << 7;
    public GameObject defeat;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(type);
        recharging = shooting_recharge;
        sh_delay = shooting_rate;
        sh_queue = shooting_queue - 1;
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = model;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
        if (speed == 0) {cell.GetComponent<Backlight>().Busy = true;}

        if (type == "Building")
        { ray_rotation = new Vector3(1, 0, 0); }
        if (type == "Enemy")
        { ray_rotation = new Vector3(-1, 0, 0); target = ~target; }

        defeat = GameObject.Find("Canvas");
    }

    void Update()
    {

        // Смэрть
        if (hp <= 0)
        {
            if (type == "Building")
            {cell.GetComponent<Backlight>().Busy = false;}
            if (type == "Enemy")
            {
                defeat.GetComponent<Defeat>().money += price;
                defeat.GetComponent<Defeat>().score += price;
            }

            Destroy(gameObject);
        }

        // Кто вышел за пределы игровой области, тот будет казнен!
        if (transform.position.x >= 2000)
        {Destroy(gameObject);}
        if (transform.position.x <= -100)
        {
            defeat.GetComponent<Defeat>().lives -= 1;
            Debug.Log("qervkgyudctyxrkyrx " + defeat.GetComponent<Defeat>().lives);
            Destroy(gameObject);
        }

        // Перемещение объекта
        if (speed != 0)
        {transform.Translate(speed * Time.deltaTime, 0, 0);}

        
        // Дальний бой
        if (shooting)
        {
            
            if (Physics.Raycast(transform.position, ray_rotation, out hit, shooting_range, target))
            {
                if (hit.transform.GetComponent<Entity>() != null)
                {
                    //if (((type == "Building" && hit.transform.GetComponent<Entity>().type == "Enemy") || (type == "Enemy" && hit.transform.GetComponent<Entity>().type == "Building")))
                    if (hit.transform.GetComponent<Entity>().hp > 0)
                    {
                        Debug.Log(hit.transform.GetComponent<Entity>().hp);
                        RangedCombat();
                    }
                }
                else
                {RangedCombat();}
            }
        }
    }

    // Стрельба
    void RangedCombat()
    {
        if (recharging <= 0)
        {
            if (sh_delay <= 0)
            {
                if (type == "Building")
                {fired_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 90, 90)));}
                if (type == "Enemy")
                {fired_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, -90, 90)));}

                fired_bullet.GetComponent<MeshFilter>().mesh = bullet_model;
                fired_bullet.GetComponent<MeshRenderer>().material = bullet_material;
                fired_bullet.GetComponent<Bullet>().sender = type;
                fired_bullet.GetComponent<Bullet>().damage = bullet_damage;
                fired_bullet.GetComponent<Bullet>().speed = bullet_speed;
                fired_bullet.GetComponent<Bullet>().boost = bullet_boost;
                fired_bullet.GetComponent<Bullet>().explosion = explosion;
                fired_bullet.GetComponent<Bullet>().expl_radius = e_radius;
                fired_bullet.GetComponent<Bullet>().expl_damage = e_damage;

                sh_delay = shooting_rate;

                if (sh_queue > 0) {sh_queue -= 1;}
                else {sh_queue = shooting_queue - 1; recharging = shooting_recharge;}
            }
            sh_delay -= Time.deltaTime;
        }
        else
        {
            recharging -= Time.deltaTime;
        }
    }

    // Действие при соприкосновении объекта с другим объектом (очевидно для арабских кораблей)
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Entity>() == null)
        {}
        else
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
                            expl_obj = Instantiate(e_prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                            expl_obj.GetComponent<Explosion>().sender = type;
                            expl_obj.GetComponent<Explosion>().radius = k_e_radius;
                            expl_obj.GetComponent<Explosion>().damage = k_e_damage;
                        }
                        cell.GetComponent<Backlight>().Busy = false;
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
                            expl_obj = Instantiate(e_prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                            expl_obj.GetComponent<Explosion>().sender = type;
                            expl_obj.GetComponent<Explosion>().radius = k_e_radius;
                            expl_obj.GetComponent<Explosion>().damage = k_e_damage;
                        }
                        //cell.GetComponent<Backlight>().Busy = false;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
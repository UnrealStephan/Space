using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int shooting_range;
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

    // Переменные для босса
    public bool boss;
    public Mesh bullet_model_b; // Параметры доп стрельбы
    public Material bullet_material_b;
    public int bullet_damage_b;
    public float bullet_speed_b;
    public float bullet_boost_b;
    public int shooting_range_b;
    public double shooting_rate_b;
    public double shooting_queue_b;
    public double shooting_recharge_b;
    public bool explosion_b;
    public int e_radius_b;
    public int e_damage_b;

    // Вспомогательные переменные (в эдиторе трогать не надо!)
    public GameObject cell;
    public GameObject bullet;
    private GameObject fired_bullet;
    private GameObject[] fired_bullet_b = new GameObject[2];
    public GameObject e_prefab;
    private GameObject expl_obj;
    private Ray sr_ray;
    private RaycastHit hit;
    private Vector3 ray_rotation;
    private double recharging;
    private double sh_delay;
    private double sh_queue;
    private double[] recharging_b = new double[2];
    private double[] sh_delay_b = new double[2];
    private double[] sh_queue_b = new double[2];
    private int target = 1 << 7;
    public GameObject defeat;
    private Vector3 shoot_pos;
    private int lines;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(type);
        recharging = shooting_recharge;
        sh_delay = shooting_rate;
        sh_queue = shooting_queue - 1;
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = model;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = material;

        if (type == "Building")
        {
            if (speed != 0) {cell.GetComponent<Backlight>().Busy = false;}
            ray_rotation = new Vector3(1, 0, 0);
        }
        if (type == "Enemy")
        {
            ray_rotation = new Vector3(-1, 0, 0); target = ~target;
        }

        defeat = GameObject.Find("Canvas");

        shoot_pos = transform.position;
        if (boss)
        {
            shoot_pos.y += 18;
        }
    }

    void Update()
    {
        if (boss)
        {defeat.transform.GetChild(2).GetComponent<Text>().text = "HP: " + hp + "/12500";}

        // Смэрть
        if (hp <= 0)
        {
            if (type == "Building")
            {cell.GetComponent<Backlight>().Busy = false;}
            if (type == "Enemy")
            {
                defeat.GetComponent<Defeat>().money += price;
            }
            if (boss)
            {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}

            Destroy(gameObject);
        }

        // Кто вышел за пределы игровой области, тот будет казнен!
        if (transform.position.x >= 160)
        {Destroy(gameObject);}
        if (transform.position.x <= -80)
        {
            if (boss) {defeat.GetComponent<Defeat>().lives -= 2;}
            defeat.GetComponent<Defeat>().lives -= 1;
            Destroy(gameObject);
        }

        // Перемещение объекта
        if (speed != 0)
        {transform.Translate(speed * Time.deltaTime, 0, 0);}

        
        // Дальний бой
        if (shooting)
        {
            if (boss) {shoot_pos.x = transform.position.x - 45;}
            else {shoot_pos.x = transform.position.x;}

            if ((Physics.Raycast(transform.position, ray_rotation, out hit, shooting_range, target)) || (Physics.Raycast(shoot_pos, ray_rotation, out hit, shooting_range, target)) && boss || (Physics.Raycast(new Vector3(shoot_pos.x,(float)(shoot_pos.y-36),shoot_pos.z), ray_rotation, out hit, shooting_range, target)) && boss)
            {
                if (hit.transform.GetComponent<Entity>() != null)
                {
                    if (hit.transform.GetComponent<Entity>().hp > 0)
                    {
                        if (boss)
                        {
                            for (int i = 1; i <= 2; i++)
                            {
                                BossRangedCombat(shoot_pos,i);
                                shoot_pos.y -= 36;
                            }
                            shoot_pos.y = transform.position.y;
                            RangedCombat(shoot_pos);
                            shoot_pos.y += 18;
                        }
                        else
                        {RangedCombat(shoot_pos);}
                    }
                }
                else
                {
                    if (boss)
                    {
                        for (int i = 1; i <= 2; i++)
                        {
                            BossRangedCombat(shoot_pos,i);
                            shoot_pos.y -= 36;
                        }
                        shoot_pos.y = transform.position.y;
                        RangedCombat(shoot_pos);
                        shoot_pos.y += 18;
                    }
                    else
                    {RangedCombat(shoot_pos);}
                }
            }
        }
    }

    // Стрельба
    void RangedCombat(Vector3 start_pos)
    {
        if (recharging <= 0)
        {
            if (sh_delay <= 0)
            {
                if (type == "Building")
                {fired_bullet = Instantiate(bullet, start_pos, Quaternion.Euler(new Vector3(0, 90, 90)));}
                if (type == "Enemy")
                {
                    fired_bullet = Instantiate(bullet, start_pos, Quaternion.Euler(new Vector3(0, -90, 90)));
                }

                fired_bullet.GetComponent<MeshFilter>().mesh = bullet_model;
                fired_bullet.GetComponent<MeshRenderer>().material = bullet_material;
                fired_bullet.GetComponent<Bullet>().sender = type;
                fired_bullet.GetComponent<Bullet>().damage = bullet_damage;
                fired_bullet.GetComponent<Bullet>().speed = bullet_speed;
                fired_bullet.GetComponent<Bullet>().boost = bullet_boost;
                fired_bullet.GetComponent<Bullet>().explosion = explosion;
                fired_bullet.GetComponent<Bullet>().expl_radius = e_radius;
                fired_bullet.GetComponent<Bullet>().expl_damage = e_damage;
                fired_bullet.GetComponent<Bullet>().boss_bullet = boss;

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

    // Доп стрельба для босса
    void BossRangedCombat(Vector3 start_pos, int i)
    {
        i -= 1;
        if (recharging_b[i] <= 0)
        {
            if (sh_delay_b[i] <= 0)
            {
                if (type == "Building")
                {fired_bullet_b[i] = Instantiate(bullet, start_pos, Quaternion.Euler(new Vector3(0, 90, 90)));}
                if (type == "Enemy")
                {
                    fired_bullet_b[i] = Instantiate(bullet, start_pos, Quaternion.Euler(new Vector3(0, -90, 90)));
                }

                fired_bullet_b[i].GetComponent<MeshFilter>().mesh = bullet_model_b;
                fired_bullet_b[i].GetComponent<MeshRenderer>().material = bullet_material_b;
                fired_bullet_b[i].GetComponent<Bullet>().sender = type;
                fired_bullet_b[i].GetComponent<Bullet>().damage = bullet_damage_b;
                fired_bullet_b[i].GetComponent<Bullet>().speed = bullet_speed_b;
                fired_bullet_b[i].GetComponent<Bullet>().boost = bullet_boost_b;
                fired_bullet_b[i].GetComponent<Bullet>().explosion = explosion_b;
                fired_bullet_b[i].GetComponent<Bullet>().expl_radius = e_radius_b;
                fired_bullet_b[i].GetComponent<Bullet>().expl_damage = e_damage_b;

                sh_delay_b[i] = shooting_rate_b;

                if (sh_queue_b[i] > 0) {sh_queue_b[i] -= 1;}
                else {sh_queue_b[i] = shooting_queue_b - 1; recharging_b[i] = shooting_recharge_b;}
            }
            sh_delay_b[i] -= Time.deltaTime;
        }
        else
        {
            recharging_b[i] -= Time.deltaTime;
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
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
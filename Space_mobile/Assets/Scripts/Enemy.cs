using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Mesh model;
    public int hp;
    public int speed;
    public int damage;
    public bool shooting;

    //data of bullet
    public Mesh bullet_model;
    public int s_speed;
    public int s_damage;
    public bool s_explosion;
    public int s_e_radius;
    public int s_e_damage;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string name;
    public int hp;
    public Mesh building_model;
    public Mesh bullet_model;
    //data of bullet
    public byte b_speed;
    public int b_damage;
    public byte b_boost;
    public bool b_explosion;
    public int b_exp_radius;
    public int b_exp_damage;
}

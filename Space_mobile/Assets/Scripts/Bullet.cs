using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Переменные снаряда

    public string sender;
    public int damage;
    public float speed;
    public float boost;
    public bool explosion;
    public GameObject e_prefab;
    private GameObject expl_obj;
    public int expl_radius;
    public int expl_damage;
    public bool disappeared = false;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(sender);
    }

    void Update()
    {
        if (boost != 0) {speed += boost * Time.deltaTime;}

        transform.Translate(0, 0, speed * Time.deltaTime);

        if ((transform.position.x >= 1500) || (transform.position.x <= -500))
        { disappeared = true; Destroy(gameObject);}
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Entity>() == null)
        {}
        else
        {
            if (sender == "Building")
            {
                if (col.gameObject.GetComponent<Entity>().type == "Enemy")
                {
                    col.gameObject.GetComponent<Entity>().hp -= damage;
                    if (explosion)
                    {
                        expl_obj = Instantiate(e_prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        expl_obj.GetComponent<Explosion>().sender = sender;
                        expl_obj.GetComponent<Explosion>().radius = expl_radius;
                        expl_obj.GetComponent<Explosion>().damage = expl_damage;
                    }
                    disappeared = true;
                    Destroy(gameObject);
                }
            }

            if (sender == "Enemy")
            {
                if (col.gameObject.GetComponent<Entity>().type == "Building")
                {
                    col.gameObject.GetComponent<Entity>().hp -= damage;
                    if (explosion)
                    {
                        expl_obj = Instantiate(e_prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        expl_obj.GetComponent<Explosion>().sender = sender;
                        expl_obj.GetComponent<Explosion>().radius = expl_radius;
                        expl_obj.GetComponent<Explosion>().damage = expl_damage;
                    }
                    disappeared = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}

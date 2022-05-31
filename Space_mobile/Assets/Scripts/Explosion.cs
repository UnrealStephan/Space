using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public string sender;
    public int radius;
    public int damage;

    void Start()
    {
        transform.GetComponent<SphereCollider>().radius = radius;
    }

    void OnCollisionEnter(Collision col)
    {
        if (sender == "Building")
        {
            if (col.gameObject.GetComponent<Entity>().type == "Enemy")
            {
                Debug.Log("Boom!");
                col.gameObject.GetComponent<Entity>().hp -= damage;
            }
        }

        if (sender == "Enemy")
        {
            if (col.gameObject.GetComponent<Entity>().type == "Building")
            {
                Debug.Log("Boom!");
                col.gameObject.GetComponent<Entity>().hp -= damage;
            }
        }
        Destroy(gameObject);
    }
}

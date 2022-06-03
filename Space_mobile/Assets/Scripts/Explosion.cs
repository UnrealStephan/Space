using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public string sender;
    public int radius;
    public int damage;
    public bool disappeared = false;

    private double time = 0.2;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(sender);
        transform.localScale *= radius;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0) { disappeared = true; Destroy(gameObject);}
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
                }
            }

            if (sender == "Enemy")
            {
                if (col.gameObject.GetComponent<Entity>().type == "Building")
                {
                    col.gameObject.GetComponent<Entity>().hp -= damage;
                }
            }
        }
        disappeared = true;
        Destroy(gameObject);
    }
}

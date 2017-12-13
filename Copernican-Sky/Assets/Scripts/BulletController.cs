using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


    private int bulletLayerNumber = 12;
    private int playerLayerNumber = 9;
    private int bullet;
    private float damage;
    private float timeAlive;


    void Start()
    {
        //Physics.IgnoreLayerCollision(bulletLayerNumber, bulletLayerNumber, true);
        Physics.IgnoreLayerCollision(bulletLayerNumber, playerLayerNumber, true);
        timeAlive = Time.time;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit");
            //if (timeAlive + 0.01f < Time.time || bullet != 99)
                Destroy(gameObject);

        }

        /*else if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieController>().Shot(damage);
            if (collision.gameObject.GetComponent<ZombieController>().getHealth() > 0)
                Destroy(gameObject);
        }
        */
    }

    public void SetBulletType(int bulletType)
    {
        bullet = bulletType;
        if (bulletType == 0)
            damage = 4.0f;
        else if (bulletType == 1)
            damage = 2.0f;
        else if (bulletType == 2)
            damage = 3.0f;
        else if (bulletType == 3)
            damage = 2.5f;
        //99 is grenade
        else if (bulletType == 99)
            damage = 2.0f;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private float damageFromBullet;

    private void Start()
    {
        health = maxHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Bullet":
                Debug.Log("Hit by bullet");
                health -= damageFromBullet;
                break;
        }

        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}

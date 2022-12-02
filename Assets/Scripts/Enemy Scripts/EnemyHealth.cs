using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private float damageFromBullet;
    [SerializeField] private ParticleSystem deathParticles;

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
                health -= damageFromBullet;
                break;
        }

        if(health <= 0)
        {
            ParticleSystem particles = Instantiate(deathParticles, transform.position, transform.rotation);
            particles.Play();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int minMoneyForKill;
    [SerializeField] private int maxMoneyForKill;
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private ParticleSystem deathParticles;

    public EnemySpawner spawner;
    public GameManager gameManager;

    private void Start()
    {
        health = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Bullet":
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                health -= bullet.bulletDamage;
                break;
        }

        if(health <= 0)
        {
            ParticleSystem particles = Instantiate(deathParticles, transform.position, transform.rotation);
            particles.Play();
            if(spawner != null)
            {
                spawner.EnemyDied();
            }
            gameManager.AddDeathMoney(minMoneyForKill, maxMoneyForKill);
            Destroy(gameObject);
        }
    }
}

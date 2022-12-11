using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int minMoneyForKill;
    [SerializeField] private int maxMoneyForKill;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private bool visuallyDamagable;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material damagedMaterial;

    [SerializeField] private ParticleSystem deathParticles;

    public EnemySpawner spawner;
    public GameManager gameManager;

    private MeshRenderer mesh;

    public bool hasChildHitBox;

    private void Start()
    {
        health = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mesh =  GetComponent<MeshRenderer>();
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

        if(health <= 50 && visuallyDamagable)
        {
            mesh.material = damagedMaterial;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void ResetHealth()
    {
        health = maxHealth;
        if (visuallyDamagable)
        {
            mesh.material = normalMaterial;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float bulletExistenceTime;
    public ParticleSystem bulletParticles;
    public float bulletDamage;

    [Header("Explosion Settings")]
    public bool explosive;
    public float explosionRadius;
    public float explosionForce;
    public float explosionLift;


    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > bulletExistenceTime)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explosive)
        {
            Explode();
        }

        ParticleSystem particles = Instantiate(bulletParticles, transform.position, transform.rotation);
        particles.Play();
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Explode()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionLift);
            }
        }

        DestroyBullet();
    }
}

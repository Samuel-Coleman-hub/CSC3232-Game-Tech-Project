using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public enum HitBoxArea
    {
        Head,
        Body
    }

    [SerializeField] HitBoxArea hitBoxArea;

    [SerializeField] Transform hitBoxParentTransform;

    private EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = hitBoxParentTransform.GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            enemyHealth.OnCollisionHitBoxEnter(hitBoxArea, bullet.bulletDamage);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] public float maxhealth;
    [SerializeField] GameManager gameManager;
    public float health;

    private void Start()
    {
        health = maxhealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.bulletDamage;
            gameManager.UpdateHealth(health);
        }

        if(health < 0f)
        {
            //End Game
            Debug.Log("Game Over");
            gameManager.GameOver();
        }
    }
}

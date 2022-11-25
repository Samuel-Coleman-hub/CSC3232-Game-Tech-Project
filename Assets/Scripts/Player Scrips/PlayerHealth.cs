using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerLives;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyAttack")
        {
            playerLives--;
            if(playerLives == 0)
            {
                PlayerDead();
            }
        }
        
    }

    private void PlayerDead()
    {
        Debug.Log("Player Dead");
    }
}

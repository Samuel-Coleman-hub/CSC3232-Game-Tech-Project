using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float bulletExistenceTime;

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
        Debug.Log("Destroy Bullet" + collision.gameObject.transform.name);
        Debug.Log(collision.gameObject.transform.tag);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

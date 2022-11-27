using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shotTip;
    private GameObject bulletContainer;

    public float shotForce;
    public float shotCooldown;

    private float timer;

    private void Awake()
    {
        bulletContainer = GameObject.FindGameObjectWithTag("BulletContainer");
    }

    public virtual void Update()
    {
        timer -= Time.deltaTime;
    }

    public void StartShooting()
    {
        if (timer <= 0)
        {
            GameObject bulletGameObject = GameObject.Instantiate(bulletPrefab, shotTip.position,
                transform.rotation, bulletContainer.transform);

            Rigidbody bulletRb = bulletGameObject.GetComponent<Rigidbody>();

            bulletRb.AddForce(shotTip.forward * shotForce, ForceMode.Impulse);
            ResetTimer();
        }

    }

    private void ResetTimer()
    {
        timer = shotCooldown;
    }
}

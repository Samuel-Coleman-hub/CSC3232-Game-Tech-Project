using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float sightRange;
    public GameObject bulletPrefab;
    public GameObject bulletContainer;
    public Transform gunTip;
    public float shotForce;
    public float gunCoolDown;

    private Rigidbody rb;
    private Rigidbody bulletRb;
    private bool playerInSight;


    private bool shooting;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //rb.velocity = transform.forward * 1000f *Time.deltaTime;

        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSight && !shooting)
        {
            StartCoroutine(StartShooting());
        }
        else
        {
            StopShooting();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Bullet")
    //    {
    //        rb.isKinematic = false;
    //    }
    //}

    public void CollisionDetection(Collision collision)
    {
        Debug.Log("hit hit box");
        if(collision.gameObject.tag == "Bullet")
        {
            rb.isKinematic = false;    
        }
    }

    IEnumerator StartShooting()
    {
        shooting = true;
        while (playerInSight)
        {
            GameObject bulletGameObject = Instantiate(bulletPrefab, gunTip.position, transform.rotation, bulletContainer.transform);
            bulletRb = bulletGameObject.GetComponent<Rigidbody>();

            bulletRb.AddForce(gunTip.forward * shotForce + rb.velocity, ForceMode.Impulse);
            yield return new WaitForSeconds(gunCoolDown);
        }
        
        shooting = false;
    }

    private void StopShooting()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Gun Settings")]
    public int mouseButtonInt;
    public float maxDistance;
    public float shotForce;
    public float gunCoolDown;
    public Transform cameraPosition;
    public Transform gunTip;
    public GameObject bulletPrefab;
    public GameObject bulletContainer;
    public Rigidbody playerRb;

    private Rigidbody bulletRb;
    private bool shooting = false;
    public ImpulseShake impulseShake;


    private void Update()
    {
        if (Input.GetMouseButtonDown(mouseButtonInt) && !shooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        GameObject bulletGameObject = Instantiate(bulletPrefab, gunTip.position, cameraPosition.rotation, bulletContainer.transform);
        bulletRb = bulletGameObject.GetComponent<Rigidbody>();

        bulletRb.AddForce(gunTip.forward * shotForce + playerRb.velocity, ForceMode.Impulse);
        //impulseShake.Shake();
        yield return new WaitForSeconds(gunCoolDown);
        shooting = false;
    }

    //private void Shoot()
    //{
    //    RaycastHit hit;
        
    //    //if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, maxDistance))
    //    //{
    //        //Debug.Log(hit.transform.name);

            
    //    //}
    //}


}

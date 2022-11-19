using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce;
    public float explosionLift;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        Debug.Log("EXPLOSION");
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        Debug.Log(colliders.Length);
        foreach(Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce * 20, explosionPosition, explosionRadius, explosionLift);
            }
            else if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionLift);
            }
        }
    }
}

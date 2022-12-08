using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] GameObject spawnObjectPrefab;
    [SerializeField] Inventory.ItemType type;
    [SerializeField] float objectHeight;
    public bool inPlayerHand = false;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !inPlayerHand)
        {
            inventory.UpdateItemCount(type, true);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground") && inPlayerHand)
        {
            GameObject item = Instantiate(spawnObjectPrefab, transform);
            item.transform.SetParent(null);
            item.transform.position = new Vector3(transform.position.x, objectHeight, transform.position.z);
            item.transform.rotation = Quaternion.identity;
            item.transform.localScale = new Vector3( 1, 1, 1);
            Destroy(gameObject);
        }
        
    }
}

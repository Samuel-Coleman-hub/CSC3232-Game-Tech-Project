using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSlot : MonoBehaviour
{
    [SerializeField] GameObject tokenPrefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float spawnForce;
    private VendingMachine parentVendingMachine;

    private void Awake()
    {
        parentVendingMachine = GetComponentInParent<VendingMachine>();
    }

    public void OnChildLookAt()
    {
        //Enabled selected effect
    }

    public void BuyItem()
    {
        Debug.Log("BOUGHT");
        GameObject token = Instantiate(tokenPrefab, spawnPoint.transform);
        Rigidbody rb = token.GetComponent<Rigidbody>();
        rb.AddForce(token.transform.forward * spawnForce, ForceMode.Impulse);
    }
}

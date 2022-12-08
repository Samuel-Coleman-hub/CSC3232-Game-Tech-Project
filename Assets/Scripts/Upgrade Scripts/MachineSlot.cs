using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineSlot : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] int itemPrice;
    [SerializeField] GameObject tokenPrefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float spawnForce;

    [SerializeField] TextMeshProUGUI buyText;

    public void OnChildLookAt()
    {
        //Enabled selected effect
        buyText.enabled = true;
    }

    public void StoppedLookingAt()
    {
        buyText.enabled = false;
        buyText.text = "E - To Buy";
    }

    public void InteractWith()
    {
        Debug.Log("BOUGHT");
        if(gameManager.GetTotalMoney() >= itemPrice)
        {
            gameManager.SubtractMoney(itemPrice);
            GameObject token = Instantiate(tokenPrefab, spawnPoint.transform);
            Rigidbody rb = token.GetComponent<Rigidbody>();
            rb.AddForce(token.transform.forward * spawnForce, ForceMode.Impulse);
        }
        else
        {
            buyText.text = "Not Enough Money";
        }

    }
}

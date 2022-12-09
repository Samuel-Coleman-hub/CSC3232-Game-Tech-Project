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

    [Header("Product Settings")]
    [SerializeField] ItemType type;
    public bool tokenPurchase;
    public bool oneTimePurchase;

    private bool outOfStock = false;
    private bool purchaseCancelled = false;

    enum ItemType
    {
        Spawnable,
        Grapple,
        Heal,
        Pads,
        Gun,
        Ufo
    }

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
        if(gameManager.GetTotalMoney() >= itemPrice && !outOfStock)
        {
            Purchase();
        }
        else if(!outOfStock)
        {
            buyText.text = "Not Enough Money";
        }

    }

    private void Purchase()
    {
        switch (type)
        {
            case ItemType.Spawnable:
                GameObject token = Instantiate(tokenPrefab, spawnPoint.transform);
                Rigidbody rb = token.GetComponent<Rigidbody>();
                rb.AddForce(token.transform.forward * spawnForce, ForceMode.Impulse);
                break;
            case ItemType.Heal:
                if (gameManager.CheckIfHealthMax())
                {
                    buyText.text = "Full Health";
                    purchaseCancelled = true;
                }
                gameManager.ResetHealth();
                break;
            case ItemType.Gun:
                GameObject gun = GameObject.FindGameObjectWithTag("Gun");
                gun.GetComponent<PlayerGun>().SwitchToLaserGun();
                break;
        }

        if (oneTimePurchase)
        {
            buyText.text = "Out Of Stock";
            outOfStock = true;
        }

        if (!purchaseCancelled)
        {
            gameManager.SubtractMoney(itemPrice);
            
        }
        purchaseCancelled = false;
    }
}

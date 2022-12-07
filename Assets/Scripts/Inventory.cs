using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEntry
{
    public InventoryEntry(Inventory.ItemType type, GameObject prefab , Image image, TextMeshProUGUI text, int num)
    {
        this.ItemType = type;
        this.ObjectPrefab = prefab;
        this.UIImage = image;
        this.UIText = text;
        this.NumberOfItem = num;
    }

    public Inventory.ItemType ItemType { get; set; }
    public GameObject ObjectPrefab { get; set; }
    public Image UIImage { get; set; }
    public TextMeshProUGUI UIText { get; set; }
    public int NumberOfItem { get; set; }
}

public class Inventory : MonoBehaviour
{
    
    List<InventoryEntry> entries = new List<InventoryEntry>();
    [Header("UI Settings")]
    [SerializeField] GameObject[] objectPrefabs;
    [SerializeField] TextMeshProUGUI[] numbersOfItemsUI;
    [SerializeField] Image[] imagesUI;
    [SerializeField] TextMeshProUGUI moneyTextUI;
    [SerializeField] TextMeshProUGUI moneyDescTextUI;
    [SerializeField] Animator moneyDescAnimator;
    private Color positiveMoneyColour = Color.cyan;
    private Color negativeMoneyColour = Color.red;

    [Header("GameObject Settings")]
    [SerializeField] Transform playerHand;

    private GameObject currentChild;
    private float imageTransparency = 0.2f;
    private int menuLocation;

    private int money = 100;

    private void Start()
    {
        int countUI = 0;
        foreach (Inventory.ItemType i in Enum.GetValues(typeof(Inventory.ItemType)))
        {
            entries.Add(new InventoryEntry(i, objectPrefabs[countUI] , imagesUI[countUI] ,numbersOfItemsUI[countUI], 0));
            countUI++;
        }

        moneyTextUI.text = "Mons: " + money.ToString();
        UpdateHand();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            menuLocation++;
            menuLocation = (menuLocation > 5 ? 0 : menuLocation);
            SwitchImageColour(1);
            UpdateHand();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            menuLocation--;
            menuLocation = (menuLocation < 0 ? 5 : menuLocation);
            SwitchImageColour(0);
            UpdateHand();
        }

        if (Input.GetMouseButtonDown(1) && currentChild != null)
        {
            playerHand.DetachChildren();
            UpdateItemCount(entries[menuLocation].ItemType, false);
            currentChild.GetComponent<Rigidbody>().isKinematic = false;
            currentChild.GetComponent<Rigidbody>().AddForce(currentChild.transform.forward * 50f, ForceMode.Impulse);
            currentChild.GetComponent<Collider>().enabled = true;
            currentChild = null;
        }
    }

    private void UpdateHand()
    {
        Debug.Log("SPAWN IN THE HAND");
        if(entries[menuLocation].NumberOfItem != 0)
        {
            GameObject child = Instantiate(entries[menuLocation].ObjectPrefab, playerHand.transform);
            child.transform.parent = playerHand.transform;
            child.GetComponent<ItemPickUp>().inPlayerHand = true;
            child.GetComponent<Rigidbody>().isKinematic = true;
            child.GetComponent<Collider>().enabled = false;
            currentChild = child;
        }
        else if(currentChild != null)
        {
            Destroy(currentChild);
            currentChild = null;
        }
    }

    private void SwitchImageColour(int up)
    {
        var tempColour = entries[menuLocation].UIImage.color;
        tempColour.a = 1f;
        entries[menuLocation].UIImage.color = tempColour;

        int imageReplacmentCount;
        if (up == 0)
        {
            imageReplacmentCount = menuLocation + 1;
        }
        else
        {
            imageReplacmentCount = menuLocation - 1;
        }

        if (imageReplacmentCount < 0)
        {
            imageReplacmentCount = 5;
        }
        else if (imageReplacmentCount > 5)
        {
            imageReplacmentCount = 0;
        }

        var tempLastColour = entries[imageReplacmentCount].UIImage.color;
        tempLastColour.a = imageTransparency;
        entries[imageReplacmentCount].UIImage.color = tempLastColour;
    }

    public enum ItemType
    {
        Turret,
        Other1,
        Other2,
        Other3,
        Other4,
        Other5,
    }

    public void UpdateItemCount(ItemType type, bool isAddition)
    {
        InventoryEntry entry = entries.Find(x => x.ItemType == type);
        if (isAddition)
        {
            entry.NumberOfItem++;
            UpdateHand();
        }
        else
        {
            entry.NumberOfItem--;
        }
        
        entry.UIText.text = entry.NumberOfItem.ToString();
        
    }

    public void UpdateMoney(int amount, bool isAddition)
    {
        
        if(!isAddition)
        {
            money -= amount;
            moneyDescTextUI.text = "-" + amount;
            moneyDescTextUI.color = negativeMoneyColour;
        }
        else
        {
            money += amount;
            moneyDescTextUI.text = "+" + amount;
            moneyDescTextUI.color = positiveMoneyColour;
        }
        moneyDescAnimator.SetTrigger("Fade");
        moneyTextUI.text = "Mons: " + money;
    }

    public int GetMoney()
    {
        return money;
    }
}

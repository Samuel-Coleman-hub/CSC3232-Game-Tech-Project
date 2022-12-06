using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryEntry
{
    public InventoryEntry(Inventory.ItemType type, TextMeshProUGUI text, int num)
    {
        this.ItemType = type;
        this.UIText = text;
        this.NumberOfItem = num;
    }

    public Inventory.ItemType ItemType { get; set; }
    public TextMeshProUGUI UIText { get; set; }
    public int NumberOfItem { get; set; }
}

public class Inventory : MonoBehaviour
{
    List<InventoryEntry> entries = new List<InventoryEntry>();
    [SerializeField] TextMeshProUGUI[] numbersOfItemsUI;

    private int menuLocation;
    private void Start()
    {
        int countUI = 0;
        foreach (Inventory.ItemType i in Enum.GetValues(typeof(Inventory.ItemType)))
        {
            entries.Add(new InventoryEntry(i, numbersOfItemsUI[countUI], 0));
            countUI++;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            
            menuLocation++;
            menuLocation = (menuLocation > 6 ? 0 : menuLocation);
            Debug.Log("SCROLLIN UP " + menuLocation);

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            menuLocation--;
            menuLocation = (menuLocation < 0 ? 6 : menuLocation);
            Debug.Log("SCROLLIN DOWN " + menuLocation);
        }
    }

    public enum ItemType
    {
        Turret,
    }

    public void AddItem(ItemType type)
    {
        InventoryEntry entry = entries.Find(x => x.ItemType == type);
        entry.NumberOfItem++;
        entry.UIText.text = entry.NumberOfItem.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEntry
{
    public InventoryEntry(Inventory.ItemType type, Image image, TextMeshProUGUI text, int num)
    {
        this.ItemType = type;
        this.UIImage = image;
        this.UIText = text;
        this.NumberOfItem = num;
    }

    public Inventory.ItemType ItemType { get; set; }
    public Image UIImage { get; set; }
    public TextMeshProUGUI UIText { get; set; }
    public int NumberOfItem { get; set; }
}

public class Inventory : MonoBehaviour
{
    List<InventoryEntry> entries = new List<InventoryEntry>();
    [SerializeField] TextMeshProUGUI[] numbersOfItemsUI;
    [SerializeField] Image[] imagesUI;

    private float imageTransparency = 0.2f;

    private int menuLocation;
    private void Start()
    {
        int countUI = 0;
        foreach (Inventory.ItemType i in Enum.GetValues(typeof(Inventory.ItemType)))
        {
            entries.Add(new InventoryEntry(i, imagesUI[countUI] ,numbersOfItemsUI[countUI], 0));
            countUI++;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            menuLocation++;
            menuLocation = (menuLocation > 5 ? 0 : menuLocation);
            Debug.Log("SCROLLIN UP " + menuLocation);
            Debug.Log(entries.Count);
            SwitchImageColour(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            menuLocation--;
            menuLocation = (menuLocation < 0 ? 5 : menuLocation);
            Debug.Log("SCROLLIN DOWN " + menuLocation);
            SwitchImageColour(0);
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
        Debug.Log("THIS IS IMAGE REPLACEMENT LOCATION" + imageReplacmentCount);

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

    public void AddItem(ItemType type)
    {
        InventoryEntry entry = entries.Find(x => x.ItemType == type);
        entry.NumberOfItem++;
        entry.UIText.text = entry.NumberOfItem.ToString();
    }
}

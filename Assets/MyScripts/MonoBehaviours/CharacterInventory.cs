using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory instance;
    public CharacterStats charStats;

    public Image[] hotBarDisplayHolders = new Image[4];
    public GameObject InventoryDisplayHolder;
    public Image[] inventoryDisplaySlots = new Image[30];

    int inventoryItempCap  =20;
    int idCount =1;
    bool addedItem = true;

    public Dictionary<int,InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    public InventoryEntry itemEntry;
    void Start()
    {
        instance = this;

        itemEntry=new InventoryEntry(0,null,null);
        itemsInInventory.Clear();
        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();

        charStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

    }

    public void StoreItem(ItemPickUp ItemToStore)
    {
        


    }
}

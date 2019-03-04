using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    #region Variable Declarations

    public static CharacterInventory instance;
    public CharacterStats charStats;
    GameObject foundStats;
    public Image[] hotBarDisplayHolders = new Image[4];
    public GameObject InventoryDisplayHolder;
    public Image[] inventoryDisplaySlots = new Image[30];

    int inventoryItempCap = 20;
    int idCount = 1;
    bool addedItem = true;

    public Dictionary<int, InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    public InventoryEntry itemEntry;

    #endregion

    #region Initializations
    void Start()
    {
        instance = this;

        itemEntry = new InventoryEntry(0, null, null);
        itemsInInventory.Clear();
        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();
        foundStats = GameObject.FindGameObjectWithTag("Player");
        charStats = foundStats.GetComponent<CharacterStats>();

    }


    #endregion


    void Update()
    {
        #region Watch for Hotbar KeyPresses - called by character controller later

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TriggerItemUse(101);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TriggerItemUse(102);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TriggerItemUse(103);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TriggerItemUse(104);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TriggerItemUse(105);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TriggerItemUse(106);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }

        #endregion

        if (!addedItem)
        {
            TryPickUp();
        }
    }

    public void StoreItem(ItemPickUp itemToStore)
    {
        addedItem = false;

        if ((charStats.characterDefinition.currentEncumbrance + itemToStore.itemDefinition.itemWeight) <=
            charStats.characterDefinition.maxEncumbrance)
        {
            itemEntry.invEntry = itemToStore;
            itemEntry.stackSize = 1;
            itemEntry.hbSprite = itemToStore.itemDefinition.itemIcon;

            itemToStore.gameObject.SetActive(false);
        }



    }

    void TryPickUp()
    {

        bool itsInInv = false;

        if (itemEntry.invEntry)
        {
            if (itemsInInventory.Count == 0)
            {
                addedItem = AddItemToInv(addedItem);
            }
            else
            {

                if (itemEntry.invEntry.itemDefinition.isStackable)
                {
                    foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
                    {
                        if (itemEntry.invEntry.itemDefinition == ie.Value.invEntry.itemDefinition)
                        {
                            ie.Value.stackSize += 1;
                            AddItemToHotBar(ie.Value);
                            itsInInv = true;
                            Object.Destroy(itemEntry.invEntry.gameObject);
                            break;
                        }
                        else
                        {
                            itsInInv = false;
                        }
                    }
                }
                else
                {
                    itsInInv = false;
                    if (itemsInInventory.Count == inventoryItempCap)
                    {
                        itemEntry.invEntry.gameObject.SetActive(true);
                        Debug.Log("Inventory is Full");
                    }
                }

                if (!itsInInv)
                {
                    addedItem = AddItemToInv(addedItem);
                    itsInInv = true;
                }
            }
        }
    }

    bool AddItemToInv(bool finishedAdding)
    {
        itemsInInventory.Add(idCount,
            new InventoryEntry(itemEntry.stackSize, Instantiate(itemEntry.invEntry), itemEntry.hbSprite));

        //charStats.characterDefinition.currentEncumbrance += itemEntry.invEntry.itemDefinition.itemWeight;
        Object.Destroy(itemEntry.invEntry.gameObject);

        FillInventoryDisplay();
        AddItemToHotBar(itemsInInventory[idCount]);

        idCount = IncreaseID(idCount);

        #region reset itemEntry

        itemEntry.invEntry = null;
        itemEntry.stackSize = 0;
        itemEntry.hbSprite = null;

        #endregion

        finishedAdding = true;
        return finishedAdding;
    }

    int IncreaseID(int currentID)
    {
        int newID = 1;
        for (int itemsCount = 1; itemsCount <= itemsInInventory.Count; itemsCount++)
        {
            if (itemsInInventory.ContainsKey(newID))
            {
                newID += 1;
            }
            else return newID;
        }

        return newID;
    }

    void AddItemToHotBar(InventoryEntry itemForHotBar)
    {
        int hotBarCounter = 0;
        bool increaseCount = false;

        foreach (Image images in hotBarDisplayHolders)
        {
            hotBarCounter += 1;
            if (itemForHotBar.hotBarSlot == 0)
            {
                if (images.sprite == null)
                {
                    //Add item to open hotbar slot
                    itemForHotBar.hotBarSlot = hotBarCounter;

                    //Change hotbar sprite to show item
                    images.sprite = itemForHotBar.hbSprite;
                    increaseCount = true;
                    break;
                }
            }
            else if (itemForHotBar.invEntry.itemDefinition.isStackable)
            {
                increaseCount = true;
            }
        }

        if (increaseCount)
        {
            hotBarDisplayHolders[itemForHotBar.hotBarSlot - 1].GetComponentInChildren<Text>().text =
                itemForHotBar.stackSize.ToString();
        }

        increaseCount = false;

    }

    void DisplayInventory()
    {
        if (InventoryDisplayHolder.activeSelf == true)
        {
            InventoryDisplayHolder.SetActive(false);
        }
        else
        {
            InventoryDisplayHolder.SetActive(true);
        }
    }

    void FillInventoryDisplay()
    {
        int slotCounter = 9;

        foreach (KeyValuePair<int,InventoryEntry> ie in itemsInInventory)
        {
            slotCounter += 1;
            inventoryDisplaySlots[slotCounter].sprite = ie.Value.hbSprite;
            ie.Value.inventorySlot = slotCounter - 9;
        }

        while (slotCounter < 29)
        {
            slotCounter++;
            inventoryDisplaySlots[slotCounter].sprite = null;
        }
    }

    public void TriggerItemUse(int itemToUseID)
    {
        bool trigerItem = false;

        foreach (KeyValuePair<int,InventoryEntry> ie in itemsInInventory)
        {
            
            if (itemToUseID > 100)
            {
                itemToUseID -= 100;
                if (ie.Value.hotBarSlot == itemToUseID)
                {
                    trigerItem = true;
                }
            }
            else
            {
                if (ie.Value.inventorySlot == itemToUseID)
                {
                    trigerItem = true;
                }
            }

            if (trigerItem)
            {
                if (ie.Value.stackSize == 1)
                {
                    if (ie.Value.invEntry.itemDefinition.isStackable)
                    {
                        if (ie.Value.hotBarSlot != 0)
                        {
                            hotBarDisplayHolders[ie.Value.hotBarSlot - 1].sprite = null;
                            hotBarDisplayHolders[ie.Value.hotBarSlot - 1].GetComponentInChildren<Text>().text = "0";
                        }
                        ie.Value.invEntry.UseItem();
                        itemsInInventory.Remove(ie.Key);
                        break;
                    }
                    else
                    {
                        ie.Value.invEntry.UseItem();
                        if (!ie.Value.invEntry.itemDefinition.isIndestructable)
                        {
                            itemsInInventory.Remove(ie.Key);
                            break;
                        }
                    }
                }
                else
                {
                    ie.Value.invEntry.UseItem();
                    ie.Value.stackSize -= 1;
                    hotBarDisplayHolders[ie.Value.hotBarSlot - 1].GetComponentInChildren<Text>().text =
                        ie.Value.stackSize.ToString();
                    break;
                }
            }
        }
        FillInventoryDisplay();
    }
}

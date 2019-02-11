using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemPickUp_SO itemDefinition;

    public CharacterStats charStats;
    CharacterInventory charInventory;

    GameObject foundStats;

    #region Constructor

    

    #endregion

    void Start()
    {
        if (charStats == null)
        {
            foundStats = GameObject.FindGameObjectWithTag("Player");
            charStats = foundStats.GetComponent<CharacterStats>();
        }
    }

    void StoreItemInInventory()
    {
        charInventory.StoreItem(this);
    }

    public void UseItem()
    {
        if(this)
        {
            switch (itemDefinition.itemType)
            {
                case ItemTypeDefinitions.HEALTH:
                    charStats.ApplyHealth(itemDefinition.itemAmount);
                    Debug.Log(charStats.GetHealth());
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinition;
    public CharacterInventory charInv;
    public GameObject characterWeaponSlot;

    #region Constructor

    public CharacterStats()
    {
        //charInv = CharacterInventory.instance;
    }

    #endregion

    #region Initialization
    void Start()
        {
            if (!characterDefinition.setManually)
            {
                characterDefinition.maxHealth = 100;
                characterDefinition.currentHealth = 50;

                characterDefinition.maxMana = 25;
                characterDefinition.currentMana = 10;

                characterDefinition.maxWealth = 500;
                characterDefinition.currentWealth = 0;

                characterDefinition.baseDamage = 2;
                characterDefinition.currentDamage = characterDefinition.baseDamage;

                characterDefinition.baseResistance = 0;
                characterDefinition.currentResistance = 0;

                characterDefinition.maxEncumbrance = 50f;
                characterDefinition.currentEncumbrance = 0;

                characterDefinition.charExperience = 0;
                characterDefinition.charLevel = 1;
            }
        }
    

    #endregion
    
}

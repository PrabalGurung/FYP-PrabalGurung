using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public string ItemType;
    BattleSystem bs;
    Save save;
    Load load;

    int index;

    public void OnClickUse()
    {
        Slot slot = GetComponent<Slot>();

        Debug.Log(slot.slotIndex);
        index = slot.slotIndex;

        if (index >= 0)
        {
            // Update the details for the selected item
            ItemType = InventoryItems._type[index];

            bs = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();

            switch (ItemType)
            {
                case "Potion":
                    bs.OnButtonPressedHealItem();
                    save = GameObject.Find("GameController").GetComponent<Save>();
                    load = GameObject.Find("GameController").GetComponent<Load>();
                    save.reduceItem();
                    load.LoadInventory();
                    break;
                case "Weapon":
                    bs.OnButtonPressedDamageItem();
                    break;
            }

        }
        else
        {
            return;
        }
    }
}

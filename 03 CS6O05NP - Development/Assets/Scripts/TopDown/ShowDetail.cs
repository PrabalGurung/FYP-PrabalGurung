using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDetail : MonoBehaviour
{
    public GameObject detailPanel;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemQuantity;
    public TextMeshProUGUI ItemType;
    public TextMeshProUGUI ItemDetail;

    int index;

    public void OnClickShow()
    {
        Slot slot = GetComponent<Slot>();

        Debug.Log(slot.slotIndex);
        index = slot.slotIndex;

        if (index >= 0)
        {
            // Update the details for the selected item
            ItemName.text = InventoryItems._name[index];
            ItemQuantity.text = InventoryItems._quantitys[index].ToString();
            ItemType.text = InventoryItems._type[index];
            ItemDetail.text = InventoryItems._description[index];
        }
        else
        {
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemText;
    public GameObject slotBattlePrefab;
    public static int slotCount;

    void Start()
    {
        for (int i = 0; i < InventoryItems._name.Count; i++)
        {
            Slot slot = Instantiate(slotBattlePrefab, inventoryPanel.transform).GetComponent<Slot>();
            slot.name = "SlotBattle" + i;
            slot.slotIndex = i;

            Debug.Log("index " + i);
            Debug.Log("ItemName" + InventoryItems._name[i]);

            itemText = slot.GetComponent<TextMeshProUGUI>();
            itemText.text = InventoryItems._name[i] + InventoryItems._quantitys[i];
        }
    }

    private void Update()
    {
        for (int i = 0; i < InventoryItems._name.Count; i++)
        {
            GameObject slot = GameObject.Find("SlotBattle" + i);

            if (slot != null)
            {
                TextMeshProUGUI item = GameObject.Find("SlotBattle" + i).GetComponent<TextMeshProUGUI>();
                item.text = InventoryItems._name[i] + InventoryItems._quantitys[i];
            }
        }
    }
}

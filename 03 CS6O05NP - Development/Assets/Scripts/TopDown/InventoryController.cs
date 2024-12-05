using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public int slotIndex;
    int i = 0;

    void Update()
    {
        while (i < slotCount)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < InventoryItems._gameObjects.Count)
            {
                GameObject item = Instantiate(InventoryItems._gameObjects[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;

                slot.slotIndex = i;
            }
            else
            {
                slot.slotIndex = -1;
            }
            Debug.Log(i);
            i++;
        }
    }
}

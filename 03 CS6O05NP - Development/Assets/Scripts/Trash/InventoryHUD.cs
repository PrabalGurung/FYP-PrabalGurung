using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryHUD : MonoBehaviour
{
    public List<InventoryItems> inventory;
    public Load script;

    void Start()
    {
        script = FindObjectOfType<Load>();
        inventory = script.inventoryList;   
    }

    //public void OnButtonClick()
    //{
    //    Debug.Log("Item: ");

    //    foreach (var item in inventory)
    //    {
    //        Debug.Log("Item: " + item.items);
    //        Debug.Log("Player: " + item.players);
    //        Debug.Log("Quantity: " + item.quantitys);
    //    }
    //}
}

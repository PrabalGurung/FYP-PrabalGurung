using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpdate : MonoBehaviour
{
    GameObject inventory;

    // Update is called once per frame
    void Update()
    {
         InventoryController inventoryController = inventory.GetComponent<InventoryController>();
    }
}

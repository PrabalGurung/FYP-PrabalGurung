using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class InventoryItemsTrash : MonoBehaviour
{
    public static List<int> items = new List<int>();
    public static List<int> players = new List<int>();
    public static List<int> quantitys = new List<int>();

    public static void Add(int item, int player, int quantity)
    {
        items.Add(item);
        players.Add(player);
        quantitys.Add(quantity);
    }

    public static void Remove(int item, int player, int quantity)
    {
        items.Remove(item);
        players.Remove(player);
        quantitys.Remove(quantity);
    }
}

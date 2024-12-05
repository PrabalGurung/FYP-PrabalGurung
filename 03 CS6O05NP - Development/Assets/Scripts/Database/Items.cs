using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public List<Item> ItemList()
    {
        return new List<Item>
        {
            new Item { Name = "Sword", Description = "A sharp blade.", Effect = 10.0f, Type = "Weapon" },
            new Item { Name = "Health Potion", Description = "Restores health.", Effect = 20.0f, Type = "Potion" },
            new Item { Name = "Shield", Description = "Blocks attacks.", Effect = 5.0f, Type = "Armor" }
        };
    }
}

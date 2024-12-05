using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItems : MonoBehaviour
{
    public static List<string> _name = new List<string>();
    public static List<string> _description = new List<string>();
    public static List<string> _type = new List<string>();
    public static List<int> _effect = new List<int>();
    public static List<int> _players = new List<int>();
    public static List<int> _quantitys = new List<int>();

    public static List<GameObject> _gameObjects = new List<GameObject>();

    public static void Add(string name, string description, int effect, string type, int player, int quantity, GameObject exists = null)
    {
        // Check if the GameObject with the same name already exists
        int existingIndex = _name.IndexOf(name);
        if (existingIndex != -1) 
        {
            // Update the existing item
            _description[existingIndex] = description;
            _effect[existingIndex] = effect;
            _type[existingIndex] = type;
            _players[existingIndex] = player;
            _quantitys[existingIndex] = quantity;

            GameObject existingItem = _gameObjects[existingIndex];
        }
        else
        {
            // Create a new item if not found
            _name.Add(name);
            _description.Add(description);
            _effect.Add(effect);
            _type.Add(type);
            _players.Add(player);
            _quantitys.Add(quantity);

            GameObject newItem = exists == null ? new GameObject(name) : exists;
            newItem.transform.position = new Vector3(0, 0, 0); 

            // Add components
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            Image imageComponent = newItem.AddComponent<Image>();
            imageComponent.color = Color.red; 

            _gameObjects.Add(newItem);
        }
    }


    public static void Remove(string name, string description, int effect, string type, int player, int quantity)
    {
        _name.Remove(name);
        _description.Remove(description);
        _effect.Remove(effect);
        _type.Remove(type);
        _players.Remove(player);
        _quantitys.Remove(quantity);
    }
}

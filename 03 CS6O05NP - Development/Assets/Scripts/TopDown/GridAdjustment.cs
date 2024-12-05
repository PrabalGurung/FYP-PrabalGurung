using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAdjustment : MonoBehaviour
{
    public GameObject gridItems;  // Array to store your grid items
    void Start()
    {
        // Make the 3rd item larger (Index 2)
        RectTransform rectTransform = gridItems.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 200); 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Save file location = C:\Users\DELL\AppData\LocalLow\DefaultCompany\My project (1)

[System.Serializable] // This attribute allows instances of SaveData to be serialized *converts an object into a format that can be easily stored
public class SaveData
{
    public Vector3 playerPosition; // Stores the player's x,y,z position
    public string mapBoundary; // Stores map name
}

/* 
Manages load system that connects to SQLite database
Works Remaining:
    - Decryption
    - Other save file extraction (After database is made)
    - ??? more ???
Problem: Doesnot load game after build (Save problem?)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using Cinemachine;
using System;

public class Load : MonoBehaviour
{
    private string connectionString;
    string dbFile = Path.Combine("Database", "saveFile.db");

    public List<InventoryItems> inventoryList = new List<InventoryItems>();

    void Start()
    {
        // Sets connection to SQLite database
        connectionString = "URI=file:" + Path.Combine(Application.dataPath, dbFile);

        loadGame();
        LoadInventory();
        //DatabaseExtraction();
    }

    public void loadGame()
    {
        // Checks if database file exist
        if (File.Exists(Path.Combine(Application.dataPath, dbFile)))
        {
            //Establish connection
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open(); // Opens connection to database

                // SQLite query to retrive player's postion and map boundary
                string query = "SELECT player_position_x, player_position_y, player_position_z, map_boundary FROM save_game LIMIT 1";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader()) //Executes query and Reads result
                    {
                        if (reader.Read()) // Checks data is null or not
                        {
                            // Collects data in variable
                            float player_position_x = reader.GetFloat(reader.GetOrdinal("player_position_x"));
                            float player_position_y = reader.GetFloat(reader.GetOrdinal("player_position_y"));
                            float player_position_z = reader.GetFloat(reader.GetOrdinal("player_position_z"));
                            string mapBoundary = reader.GetString(reader.GetOrdinal("map_boundary"));

                            // Find the gameobject with tag "Player" and sets the position from database
                            GameObject player = GameObject.FindWithTag("Player");
                            player.transform.position = new Vector3(player_position_x, player_position_y, player_position_z);

                            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(mapBoundary).GetComponent<PolygonCollider2D>();
                        }
                    }
                }
            }
        }
    }

    public void LoadInventory()
    {
        // Checks if database file exist
        if (File.Exists(Path.Combine(Application.dataPath, dbFile)))
        {
            //Establish connection
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open(); // Opens connection to database
                string query = "SELECT i.Name, i.Description, i.Effect, i.Type, n.Player, n.Quantity FROM inventory n JOIN items i ON i.item_id = n.Item";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader()) //Executes query and Reads result
                    {
                        while (reader.Read()) // Iterate through all rows in the result set
                        {
                            // Read data
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string description = reader.GetString(reader.GetOrdinal("Description"));
                            string type = reader.GetString(reader.GetOrdinal("Type"));
                            int effect = reader.GetInt32(reader.GetOrdinal("Effect"));
                            int player = reader.GetInt32(reader.GetOrdinal("Player"));
                            int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        
                            InventoryItems.Add(name, description, effect, type, player, quantity);
                        }
                    }
                }
            }
        }
    }

    private void DatabaseExtraction()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // Query to check if the 'items' table exists in the database
            string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='items';";

            using (var command = new SqliteCommand(checkTableQuery, connection))
            {
                var tableName = command.ExecuteScalar(); 

                if (tableName == null)
                {
                    Debug.Log("Table 'items' does not exist.");
                    return;
                }
                else
                {
                    Debug.Log("Table 'items' exists. Retrieving data...");

                    string query = "SELECT * FROM items;";

                    using (var dataCommand = new SqliteCommand(query, connection))
                    {
                        using (var reader = dataCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Debug.Log($"{reader.GetName(i)}: {reader.GetValue(i)}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

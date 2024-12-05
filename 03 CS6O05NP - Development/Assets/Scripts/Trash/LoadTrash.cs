using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadTrash : MonoBehaviour
{
    //public void LoadInventory()
    //{
    //    // Checks if database file exist
    //    if (File.Exists(Path.Combine(Application.dataPath, dbFile)))
    //    {
    //        //Establish connection
    //        using (var connection = new SqliteConnection(connectionString))
    //        {
    //            connection.Open(); // Opens connection to database
    //            string query = "SELECT * FROM inventory";
    //            using (var command = new SqliteCommand(query, connection))
    //            {
    //                using (var reader = command.ExecuteReader()) //Executes query and Reads result
    //                {
    //                    while (reader.Read()) // Iterate through all rows in the result set
    //                    {
    //                        // Read the data from the database
    //                        int item = reader.GetInt32(reader.GetOrdinal("Item"));
    //                        int player = reader.GetInt32(reader.GetOrdinal("Player"));
    //                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));

    //                        // add it to the list
    //                        InventoryItems.Add(item, player, quantity);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //public void LoadItem()
    //{
    //    // Checks if database file exist
    //    if (File.Exists(Path.Combine(Application.dataPath, dbFile)))
    //    {
    //        //Establish connection
    //        using (var connection = new SqliteConnection(connectionString))
    //        {
    //            connection.Open(); // Opens connection to database
    //            string query = "SELECT * FROM inventory";
    //            using (var command = new SqliteCommand(query, connection))
    //            {
    //                using (var reader = command.ExecuteReader()) //Executes query and Reads result
    //                {
    //                    while (reader.Read()) // Iterate through all rows in the result set
    //                    {
    //                        // Read the data from the database
    //                        int item = reader.GetInt32(reader.GetOrdinal("Item"));
    //                        int player = reader.GetInt32(reader.GetOrdinal("Player"));
    //                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));

    //                        // add it to the list
    //                        InventoryItems.Add(item, player, quantity);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

}

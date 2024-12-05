/* 
Handles saving game data to SQLite database
Current Entity made:    - Part of Option (as save_game)
Works Remaining:       
    - Encryption
    - More entity needs to be added (After game progresses)
Problems:               - After build does not save properly (Path problem?) (DONE)
                        - Database not found but saves properly

Table Added (Items, SaveFile)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;
using System.Text;

public class Save : MonoBehaviour
{
    private string connectionString;

    private void Start()
    {
        // Creates or Opens (If exist) 
        string dbFile = Path.Combine("Database", "saveFile.db");
        //Path connectionString = "URI=file:" + Application.dataPath + dbFile;
        connectionString = "URI=file:" + Path.Combine(Application.dataPath, dbFile);
    }

    // Calls when save button is clicked
    public void OnButtonClick()
    {
        // Creates or Opens (If exist) 
        string dbFile = Path.Combine("Database", "saveFile.db");
        //Path connectionString = "URI=file:" + Application.dataPath + dbFile;
        connectionString = "URI=file:" + Path.Combine(Application.dataPath, dbFile);

        bool save_exist = CheckSave();

        // Checks if save file and approprate ID exist or not 
        if (save_exist)
        {
            bool check_id = CheckId();

            if (check_id)
            {
                UpdateSave();
            }
            else
            {
                SaveGame();
            }
        }
        else
        {
            CreateTable();
        }
    }

    public void MediKit()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            Debug.Log(connection.ToString());

            connection.Open();

            // Check if the item already exists for the player
            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM inventory WHERE item = @item AND player = @player";

            checkCmd.Parameters.AddWithValue("@item", 2);
            checkCmd.Parameters.AddWithValue("@player", 1);

            int itemExists = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (itemExists > 0)
            {
                var updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "UPDATE inventory SET quantity = quantity + @quantity WHERE item = @item AND player = @player";

                updateCmd.Parameters.AddWithValue("@item", 2);
                updateCmd.Parameters.AddWithValue("@player", 1);
                updateCmd.Parameters.AddWithValue("@quantity", 1);

                updateCmd.ExecuteNonQuery();
            }
            else
            {
                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO inventory (item, player, quantity) VALUES (@item, @player, @quantity)";

                insertCmd.Parameters.AddWithValue("@item", 2);
                insertCmd.Parameters.AddWithValue("@player", 1);
                insertCmd.Parameters.AddWithValue("@quantity", 1);

                insertCmd.ExecuteNonQuery();
            }
        }
    }

    public void reduceItem()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT quantity FROM inventory WHERE item = @item AND player = @player";

            checkCmd.Parameters.AddWithValue("@item", 2);
            checkCmd.Parameters.AddWithValue("@player", 1);

            int quantity = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (quantity > 0) 
            {
                var decreaseItem = connection.CreateCommand();

                decreaseItem.CommandText = "UPDATE inventory SET quantity = quantity - 1 WHERE item = @item AND player = @player";
                decreaseItem.Parameters.AddWithValue("@item", 2);  
                decreaseItem.Parameters.AddWithValue("@player", 1);  

                decreaseItem.ExecuteNonQuery();  

            }
            else if (quantity <= 0)
            {
                 var deleteItem = connection.CreateCommand();
                deleteItem.CommandText = "DELETE FROM inventory WHERE item = @item AND player = @player";
                deleteItem.Parameters.AddWithValue("@item", 2); 
                deleteItem.Parameters.AddWithValue("@player", 1);  

                deleteItem.ExecuteNonQuery();
            }
        }
    }

    // Creates a new save
    public void SaveGame()
    {
        // Create a new SaveData instance to store current position 
        var saveData = new SaveData
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name
        };

        using (var connection = new SqliteConnection(connectionString))

        {
            connection.Open();
            var insertCmd = connection.CreateCommand();

            // SQLite query to insert value
            insertCmd.CommandText = @"
                INSERT INTO save_game (player_position_x, player_position_y, player_position_z, map_boundary)
                VALUES (@x, @y, @z, @boundary)";

            insertCmd.Parameters.AddWithValue("@x", saveData.playerPosition.x);
            insertCmd.Parameters.AddWithValue("@y", saveData.playerPosition.y);
            insertCmd.Parameters.AddWithValue("@z", saveData.playerPosition.z);
            insertCmd.Parameters.AddWithValue("@boundary", saveData.mapBoundary);

            insertCmd.ExecuteNonQuery(); // Executes SQLite query without returning any value
        }
    }

    // Updates the existing save file with the new one
    public void UpdateSave()
    {
        var saveData = new SaveData
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name
        };

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var insertCmd = connection.CreateCommand();

            // SQLite query to update table specific rows and columns @var later determines which value to insert
            insertCmd.CommandText = @"
                UPDATE save_game SET player_position_x = @x, player_position_y = @y, player_position_z = @z, map_boundary = @boundary 
                WHERE id = 1;";

            insertCmd.Parameters.AddWithValue("@x", saveData.playerPosition.x);
            insertCmd.Parameters.AddWithValue("@y", saveData.playerPosition.y);
            insertCmd.Parameters.AddWithValue("@z", saveData.playerPosition.z);
            insertCmd.Parameters.AddWithValue("@boundary", saveData.mapBoundary);

            insertCmd.ExecuteNonQuery();
        }
    }

    // Checks if save_game table exists in database or not
    private bool CheckSave()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // SQLite query to lookup for table with appropriate name in database
            string query = "SELECT name FROM sqlite_master WHERE type='table' AND name='save_game';";

            using (var command = new SqliteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if any table is detected
                }
            }
        }
    }

    // Checks ID and returns true or false based on answer
    private bool CheckId()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT 1 FROM save_game WHERE id = 1;";
            using (var command = new SqliteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }
    }

    // Creates a table to store data
    private void CreateTable()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE save_game (id INTEGER PRIMARY KEY AUTOINCREMENT, player_position_x REAL, player_position_y REAL, player_position_z REAL, map_boundary TEXT)";
                command.ExecuteNonQuery();
            }
            using (var skillCommand = connection.CreateCommand())
            {
                skillCommand.CommandText = "CREATE TABLE skills (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Description TEXT, Effect INTEGER, Attribute TEXT)";
                skillCommand.ExecuteNonQuery();
            }
            using (var itemCommand = connection.CreateCommand())
            {
                itemCommand.CommandText = "CREATE TABLE items (item_id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Description TEXT, Effect INTEGER, Type TEXT)";
                itemCommand.ExecuteNonQuery();
            }
            using (var playerCommand = connection.CreateCommand())
            {
                playerCommand.CommandText = "CREATE TABLE player (id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Attribute TEXT, LEVEL INT, Skill INTEGER, Exp INTEGER, Health INTEGER, Mana INTEGER, C_Health INTEGER, C_Mana INTEGER,  FOREIGN KEY (Skill) REFERENCES skills(skill_id))";
                playerCommand.ExecuteNonQuery();
            }
            using (var playerItem = connection.CreateCommand())
            {
                playerItem.CommandText = "CREATE TABLE inventory (id INTEGER PRIMARY KEY AUTOINCREMENT, Item INTEGER, Player INTEGER, Quantity INTEGER, FOREIGN KEY (Player) REFERENCES player(id), FOREIGN KEY (Item) REFERENCES items(item_id))";
                playerItem.ExecuteNonQuery();
            }
            Items items = new Items();
            InsertMultipleItems(connection, items.ItemList());
        }
    }

    private void InsertMultipleItems(SqliteConnection connection, List<Item> items)
    {
        using (var transaction = connection.BeginTransaction())
        {
            using (var command = connection.CreateCommand())
            {
                var insertValues = new StringBuilder();

                for (int i = 0; i < items.Count; i++)
                {
                    insertValues.Append($@"(@Name{i}, @Description{i}, @Effect{i}, @Type{i}),");

                    command.Parameters.AddWithValue($"@Name{i}", items[i].Name);
                    command.Parameters.AddWithValue($"@Description{i}", items[i].Description);
                    command.Parameters.AddWithValue($"@Effect{i}", items[i].Effect);
                    command.Parameters.AddWithValue($"@Type{i}", items[i].Type);
                }

                // Remove the trailing comma
                if (items.Count > 0)
                {
                    insertValues.Length--;
                }

                command.CommandText = $@"
                    INSERT INTO items (Name, Description, Effect, Type) 
                    VALUES {insertValues}";

                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

    }
}



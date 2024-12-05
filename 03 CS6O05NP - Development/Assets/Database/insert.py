import sqlite3

# Connect to SQLite database (it will create the database if it doesn't exist)
conn = sqlite3.connect('saveFile.db')

# Create a cursor object to interact with the database
cursor = conn.cursor()

# Insert a record into the table (Make sure your table exists with the right structure)
cursor.execute(
    "INSERT INTO player (name, attribute, level, exp, health, mana, c_health, c_mana) VALUES (?, ?, ?, ?, ?, ?, ?, ?)",
    ("Setsu", "Water", 30, 20, 200, 180, 200, 180)
)

# Commit the transaction
conn.commit()

# Close the connection
conn.close()

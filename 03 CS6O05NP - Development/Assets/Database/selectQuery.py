import sqlite3

# Connect to the SQLite database
conn = sqlite3.connect('saveFile.db')
c = conn.cursor()

# Define the SQL query
query = "SELECT * FROM inventory;"

# Execute the query
c.execute(query)

# Fetch all results and print them
results = c.fetchall()
for row in results:
    print(row)

# Commit any changes (although no changes are made in this query)
conn.commit()

# Close the connection
conn.close()

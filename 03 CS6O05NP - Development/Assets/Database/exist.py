import sqlite3

conn = sqlite3.connect('saveFile.db')

c = conn.cursor()

c.execute("SELECT name FROM sqlite_master WHERE type='table' AND name='save_game';")
c.execute("SELECT 1 FROM save_game WHERE id = 1;")



print(c.fetchall())

conn.commit()

conn.close()
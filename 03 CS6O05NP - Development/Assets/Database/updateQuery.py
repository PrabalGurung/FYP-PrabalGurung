import sqlite3

conn = sqlite3.connect('saveFile.db')

c = conn.cursor()

c.execute("UPDATE save_game SET player_position_x = 0.2649999260902405, player_position_y = 14.904863357543945, player_position_z = 0.0, map_boundary = 'F1' WHERE id = 1;")

conn.commit()

conn.close()
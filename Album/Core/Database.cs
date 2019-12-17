using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Album.Core
{
    class Database
    {
        private const string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [photos] (
                                               [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                               [name] VARCHAR(100)  NULL,
                                               [data] LONGTEXT  NULL
                                               )";

        private const string DatabaseFile = "PhotoDB.db";
        private const string DatabaseSource = "data source=" + DatabaseFile;
        private SQLiteConnection DBConnection;
        public main App;
        public Database(main app)
        {
            App = app;
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
            }

            DBConnection = new SQLiteConnection(DatabaseSource);

            using (var command = new SQLiteCommand(DBConnection))
            {
                DBConnection.Open();
                command.CommandText = CreateTableQuery;
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }

        public void Load()
        {
            using (var command = new SQLiteCommand(DBConnection))
            {
                DBConnection.Open();
                command.CommandText = "Select * FROM photos";
                using (var reader = command.ExecuteReader())
                {
                    Dictionary<string, string> tempList = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        tempList[reader["name"].ToString()] = reader["data"].ToString();
                    }
                    App.UpdateImages(tempList);
                }
                DBConnection.Close();
            }
        }

        public void Add(string name, string data)
        {
            using (var command = new SQLiteCommand(DBConnection))
            {
                DBConnection.Open();
                command.CommandText = "INSERT INTO photos (name,data) VALUES ('" + name + "','" + data + "')";
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }

        public void Remove(string name)
        {
            using (var command = new SQLiteCommand(DBConnection))
            {
                DBConnection.Open();
                command.CommandText = "DELETE FROM photos WHERE name='" + name + "'";
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }
    }
}

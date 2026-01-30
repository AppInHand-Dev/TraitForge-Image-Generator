using System.Data.SQLite;
using System.IO;
using System.Transactions;

namespace TraitForge
{
    public class DatabaseManager
    {

        public SQLiteConnection DbConnection { get; set; }


        public DatabaseManager(string dbName)
        {
            if (!File.Exists($"{dbName}.sqlite"))
            {
                CreateDb(dbName);
            }
        }

        public static void CreateDb(string dbName)
        {
            SQLiteConnection.CreateFile($"{dbName}.sqlite");
        }

        public void ConnectToDb(string dbName)
        {
            string connectionString = $"data source={dbName}.sqlite;Version=3;foreign keys=true;";
            DbConnection = new SQLiteConnection(connectionString);
        }

        public void InitDb()
        {
            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {
                string sql =
                    "CREATE TABLE IF NOT EXISTS collections" +
                    "(" +
                        "id INTEGER NOT NULL PRIMARY KEY," +
                        "name VARCHAR(20) NOT NULL UNIQUE," +
                        "description VARCHAR(20)" +
                    ")";

                SQLiteCommand command = new SQLiteCommand(sql, DbConnection);
                //command.ExecuteReader();
                command.ExecuteNonQuery();

                sql =
                    "CREATE TABLE IF NOT EXISTS trait_types" +
                    "(" +
                        "id INTEGER NOT NULL PRIMARY KEY," +
                        "name VARCHAR(20) NOT NULL," +
                        "_order INTEGER NOT NULL," +
                        "active BOOLEAN," +
                        "collectionId INTEGER NOT NULL," +
                        "CONSTRAINT fk_collectionId " +
                            "FOREIGN KEY(collectionId) " +
                            "REFERENCES collections(id) " +
                            "ON DELETE CASCADE" +
                    ")";

                command = new SQLiteCommand(sql, DbConnection);
                command.ExecuteNonQuery();
                //command.CommandText = sql;

                sql =
                    "CREATE TABLE IF NOT EXISTS traits " +
                    "(" +
                        "id INTEGER NOT NULL PRIMARY KEY," +
                        "name varchar(20) NOT NULL," +
                        "raw INTEGER, active BOOLEAN," +
                        "traitTypeId INTEGER NOT NULL," +
                        "CONSTRAINT fk_traitTypeId " +
                            "FOREIGN KEY(traitTypeId)" +
                            "REFERENCES trait_types(id)" +
                            "ON DELETE CASCADE" +
                    ")";

                command = new SQLiteCommand(sql, DbConnection);
                command.ExecuteNonQuery();

                /*sql = "Insert into highscores (name, score) values ('Me', 9001)";
                command = new SQLiteCommand(sql, DbConnection);
                command.ExecuteNonQuery();*/

                transaction.Complete();
            }

            DbConnection.Close();
        }
    }

}

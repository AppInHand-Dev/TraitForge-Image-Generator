using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TraitForge
{
    public class Class1
    {

        public SQLiteConnection DbConnection { get; set; }


        public Class1()
        {}


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
                    "CREATE TABLE collections" +
                    "(" +
                        "id INTEGER NOT NULL PRIMARY KEY," +
                        "name VARCHAR(20) NOT NULL UNIQUE," +
                        "description VARCHAR(20)" +
                    ")";

                SQLiteCommand command = new SQLiteCommand(sql, DbConnection);
                //command.ExecuteReader();
                command.ExecuteNonQuery();

                sql = 
                    "CREATE TABLE trait_types" +
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
                    "CREATE TABLE traits " +
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

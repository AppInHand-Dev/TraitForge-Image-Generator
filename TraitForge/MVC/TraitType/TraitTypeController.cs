using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TraitForge.MVC.TraitType
{
    class TraitTypeController
    {

        public static TraitType TraitType { get; set; }

        public static List<TraitType> GetList(SQLiteConnection DbConnection)
        {

            List<TraitType> traitTypes = new List<TraitType>();

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM trait_types;
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt16(0);
                        var name = reader.GetString(1);
                        var order = reader.GetInt16(2);
                        var active = reader.GetBoolean(3);
                        var collectionId = reader.GetInt16(4);

                        traitTypes.Add(new TraitType(id, name, order, active, collectionId));
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return traitTypes;

        }

        public static List<TraitType> GetList(SQLiteConnection DbConnection, int collectionId)
        {

            List<TraitType> traitTypes = new List<TraitType>();

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();

                command.CommandText =
                @"
                    SELECT *
                    FROM trait_types
                    WHERE collectionId=$collectionId;
                ";

                command.Parameters.AddWithValue("$collectionId", collectionId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt16(0);
                        var name = reader.GetString(1);
                        var order = reader.GetInt16(2);
                        var active = reader.GetBoolean(3);

                        traitTypes.Add(new TraitType(id, name, order, active, collectionId));
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return traitTypes;

        }

        public static void CreateNew(SQLiteConnection DbConnection, TraitType traitType)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                    @"
                        INSERT INTO trait_types(name, active, _order, collectionId) 
                        SELECT $name, $active, $order, $collectionId 
                        WHERE NOT EXISTS(SELECT 1 FROM trait_types WHERE collectionId = $collectionId AND name = $name);
                    ";
                command.Parameters.AddWithValue("$name", traitType.Name);
                command.Parameters.AddWithValue("order", traitType.Order);
                command.Parameters.AddWithValue("active", traitType.Active);
                command.Parameters.AddWithValue("$collectionId", traitType.CollectionId);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

        public static TraitType GetById(SQLiteConnection DbConnection, int id)
        {

            string name = "";
            int order = 0;
            bool active = false;
            int collectionId = 0;

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM trait_types
                    WHERE id=$id
                    LIMIT 1;
                ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1);
                        order = reader.GetInt16(2);
                        active = reader.GetBoolean(3);
                        collectionId = reader.GetInt16(4);
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return new TraitType(id, name, order, active, collectionId);

        }

        public static void Delete(SQLiteConnection DbConnection, TraitType traitType)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM trait_types
                    WHERE id=$id;
                ";
                command.Parameters.AddWithValue("$id", traitType.Id);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

        public static void Duplicate(TraitType traitType)
        {
        }

        public static void Edit(SQLiteConnection DbConnection, TraitType traitType)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE trait_types
                    SET name=$name
                    WHERE id=$id;
                ";
                command.Parameters.AddWithValue("$name", traitType.Name);
                command.Parameters.AddWithValue("$id", traitType.Id);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

    }
}

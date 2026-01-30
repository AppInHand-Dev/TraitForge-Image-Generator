using System.Data.SQLite;
using System.Transactions;

namespace TraitForge.MVC.Trait
{
    class TraitController
    {

        public static Trait Trait { get; set; }

        public static List<Trait> GetList(SQLiteConnection DbConnection)
        {

            List<Trait> traits = new List<Trait>();

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM traits;
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt16(0);
                        var name = reader.GetString(1);
                        var raw = reader.GetInt16(2);
                        var active = reader.GetBoolean(3);
                        var categoryId = reader.GetInt16(4);

                        traits.Add(new Trait(id, name, raw, active, categoryId));
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return traits;

        }

        public static List<Trait> GetList(SQLiteConnection DbConnection, int traitTypeId)
        {

            List<Trait> traits = new List<Trait>();

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();

                command.CommandText =
                @"
                    SELECT *
                    FROM traits
                    WHERE traitTypeId=$traitTypeId;
                ";

                command.Parameters.AddWithValue("$traitTypeId", traitTypeId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt16(0);
                        var name = reader.GetString(1);
                        var raw = reader.GetInt16(2);
                        var active = reader.GetBoolean(3);

                        traits.Add(new Trait(id, name, raw, active, traitTypeId));
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return traits;

        }

        public static void CreateNew(SQLiteConnection DbConnection, Trait trait)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO traits (name, raw, active, traitTypeId)
                    SELECT $name, $raw, $active, $traitTypeId 
                    WHERE NOT EXISTS(SELECT 1 FROM traits WHERE traitTypeId = $traitTypeId AND name = $name);
                    VALUES ($name, $raw, $active, $traitTypeId);
                ";
                command.Parameters.AddWithValue("$name", trait.Name);
                command.Parameters.AddWithValue("$raw", trait.Raw);
                command.Parameters.AddWithValue("active", trait.Active);
                command.Parameters.AddWithValue("$traitTypeId", trait.TraitTypeId);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

        public static Trait GetById(SQLiteConnection DbConnection, int id)
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

            return new Trait(id, name, order, active, collectionId);

        }

        public static void Delete(SQLiteConnection DbConnection, Trait traitType)
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

        public static void Duplicate(Trait traitType)
        {
        }

        public static void Edit(SQLiteConnection DbConnection, Trait traitType)
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

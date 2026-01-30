using System.Data.SQLite;
using System.Transactions;

namespace TraitForge.MVC.Collection
{
    class CollectionController
    {

        public static Collection Collection { get; set; }

        public static List<Collection> GetList(SQLiteConnection DbConnection)
        {

            List<Collection> collections = new List<Collection>();

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM collections;
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt16(0);
                        var name = reader.GetString(1);
                        var description = reader.GetString(2);

                        collections.Add(new Collection(id, name, description));
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return collections;

        }

        public static void CreateNew(SQLiteConnection DbConnection, Collection collection)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO collections (name, description)
                    VALUES ($name, $description);
                ";
                command.Parameters.AddWithValue("$name", collection.Name);
                command.Parameters.AddWithValue("$description", collection.Description);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

        public static Collection GetById(SQLiteConnection DbConnection, int id)
        {

            string name = "";
            string description = "";

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {


                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM collections
                    WHERE id=$id
                    LIMIT 1;
                ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1);
                        description = reader.GetString(2);
                    }
                }

                transaction.Complete();
            }

            DbConnection.Close();

            return new Collection(id, name, description);

        }

        public static void Delete(SQLiteConnection DbConnection, Collection collection)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM collections
                    WHERE id=$id;
                ";
                command.Parameters.AddWithValue("$id", collection.Id);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

        public static void Duplicate(Collection collection)
        {
        }

        public static void Edit(SQLiteConnection DbConnection, Collection collection)
        {

            DbConnection.Open();

            using (TransactionScope transaction = new TransactionScope())
            {

                var command = DbConnection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE collections
                    SET name=$name, description=$description
                    WHERE id=$id;
                ";
                command.Parameters.AddWithValue("$name", collection.Name);
                command.Parameters.AddWithValue("$description", collection.Description);
                command.Parameters.AddWithValue("$id", collection.Id);

                command.ExecuteNonQuery();

                transaction.Complete();
            }

            DbConnection.Close();

        }

    }
}

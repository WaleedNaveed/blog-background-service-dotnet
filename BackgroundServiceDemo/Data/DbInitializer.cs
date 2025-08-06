using BackgroundServiceDemo.Helper;
using Microsoft.Data.Sqlite;

namespace BackgroundServiceDemo.Data
{
    public static class DbInitializer
    {
        private const string DbPath = Constants.DatabaseName;

        public static void Initialize()
        {
            if (!File.Exists(DbPath))
            {
                using var connection = new SqliteConnection($"Data Source={DbPath}");
                connection.Open();

                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT,
                        LastName TEXT,
                        Email TEXT,
                        Gender TEXT,
                        IpAddress TEXT
                    );";
                tableCommand.ExecuteNonQuery();
            }
        }
    }
}

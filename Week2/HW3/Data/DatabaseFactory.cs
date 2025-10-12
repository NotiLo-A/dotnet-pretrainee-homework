using Microsoft.Data.Sqlite;
using System.Data;

namespace HW3.Data;

public static class DatabaseFactory
{
    private const string ConnectionString = "Data Source=HW3.db";

    public static IDbConnection CreateConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }
    
    public static void InitDb()
    {
        using var connection = CreateConnection();
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS Tasks (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Description TEXT,
            IsCompleted INTEGER NOT NULL,
            CreatedAt TEXT NOT NULL
        );";
        cmd.ExecuteNonQuery();
    }
}
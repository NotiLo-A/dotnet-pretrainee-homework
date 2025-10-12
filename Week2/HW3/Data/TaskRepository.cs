using Dapper;
using HW3.Models;

namespace HW3.Data;

public class TaskRepository
{
    public void Add(TaskItem task)
    {
        using var connection = DatabaseFactory.CreateConnection();
        string sql = "INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";
        connection.Execute(sql, task);
    }

    public IEnumerable<TaskItem> GetAll()
    {
        using var connection = DatabaseFactory.CreateConnection();
        return connection.Query<TaskItem>("SELECT * FROM Tasks ORDER BY CreatedAt DESC");
    }

    public void UpdateStatus(int id, bool isCompleted)
    {
        using var connection = DatabaseFactory.CreateConnection();
        connection.Execute("UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id", new { Id = id, IsCompleted = isCompleted });
    }

    public void Delete(int id)
    {
        using var connection = DatabaseFactory.CreateConnection();
        connection.Execute("DELETE FROM Tasks WHERE Id = @Id", new { Id = id });
    }
}
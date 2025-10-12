using HW3.Data;
using HW3.Models;

namespace HW3;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        SetupExitHandler();

        DatabaseFactory.InitDb();
        var repo = new TaskRepository();

        var menu = new Menu("Add New Task", "View All Tasks", "Update Task Status", "Delete Task by Id", "Exit");

        bool running = true;

        while (running)
        {
            int selected = menu.Run();
            Console.Clear();
            Console.CursorVisible = true;
            
            switch (menu.Options[selected])
            {
                case "Add New Task":
                    AddTask(repo);
                    break;

                case "View All Tasks":
                    ViewTasks(repo);
                    break;

                case "Update Task Status":
                    UpdateTask(repo);
                    break;

                case "Delete Task by Id":
                    DeleteTask(repo);
                    break;

                case "Exit":
                    running = false;
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(true);
                Console.CursorVisible = false;
                Console.Clear();
            }
        }

        Console.CursorVisible = true;
    }

    static void W(string text, ConsoleColor color, bool newline = true)
    {
        Console.ForegroundColor = color;
        if (newline) Console.WriteLine(text);
        else Console.Write(text);
        Console.ResetColor();
    }
    
    static void AddTask(TaskRepository repo)
    {
        W("< Title >", ConsoleColor.DarkGray);
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            W("\nTitle is required!", ConsoleColor.Red);
            return;
        }

        W("< Description > (Empty line = finish)", ConsoleColor.DarkGray);
        var lines = new List<string>();
        while (true)
        {
            string? line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) break;
            lines.Add(line);
        }
        
        W("\nSave task? (Y/n): ", ConsoleColor.DarkGray, false);
        var input = Console.ReadKey().Key;
        if (input != ConsoleKey.Y && input != ConsoleKey.Enter)
        {   
            W("\nTask not added", ConsoleColor.Red);
            return;
        }
        
        string desc = string.Join(Environment.NewLine, lines);

        var task = new TaskItem
        {
            Title = title,
            Description = desc,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };
        
        repo.Add(task);
        W("\nTask added!", ConsoleColor.Green);
    }

    static void ViewTasks(TaskRepository repo)
    {
        var tasks = repo.GetAll().ToList();
        if (!tasks.Any())
        {
            W("No tasks found.", ConsoleColor.Red);
            return;
        }
        Console.WriteLine("[ID] status | date");
        Console.WriteLine(new string('═', 60));
        foreach (var t in tasks)
        {
            W($"[{t.Id}] ", ConsoleColor.DarkGray, false);
            W(t.IsCompleted ? "Done" : "Pending", t.IsCompleted ? ConsoleColor.Green : ConsoleColor.Red, false);
            W($" | {t.CreatedAt:g}", ConsoleColor.DarkGray);
            Console.WriteLine($"{t.Title}\n");
            
            if (!string.IsNullOrWhiteSpace(t.Description))
                Console.WriteLine($"{t.Description}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('-', 60));
            Console.ResetColor();

        }
    }

    static void UpdateTask(TaskRepository repo)
    {
        Console.CursorVisible = false;
        var tasks = repo.GetAll().ToList();
        if (!tasks.Any())
        {
            W("No tasks found.", ConsoleColor.Red);
            return;
        }

        int selected = 0;
        var tempStatuses = tasks.Select(t => t.IsCompleted).ToList();

        void Draw()
        {
            Console.Clear();
            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks[i];
                string title = t.Title.Length > 17 ? t.Title[..17] + "..." : t.Title.PadRight(20);
                string status = tempStatuses[i] ? "Done" : "Pending";

                if (i == selected)
                    W($"{title} < [{status}] >", ConsoleColor.Green);
                else
                    Console.WriteLine($"{title} < [{status}] >");
            }
            W("\nUse ↑ ↓ to select a task, ← → to toggle status.\nPress Enter to save changes (you'll be asked to confirm).", ConsoleColor.DarkGray);
        }

        while (true)
        {
            Draw();
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + tasks.Count) % tasks.Count;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % tasks.Count;
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow) tempStatuses[selected] = !tempStatuses[selected];
            else if (key == ConsoleKey.Enter)
            {
                W("\nSave task status changes? (Y/n): ", ConsoleColor.DarkGray, false);
                var input = Console.ReadKey().Key;
                if (input == ConsoleKey.Y || input == ConsoleKey.Enter)
                {
                    for (int i = 0; i < tasks.Count; i++)
                        if (tasks[i].IsCompleted != tempStatuses[i])
                            repo.UpdateStatus(tasks[i].Id, tempStatuses[i]);
                    W("\nChanges saved!", ConsoleColor.Green);
                }
                else
                    W("\nChanges canceled.", ConsoleColor.Red);
                return;
            }
        }
    }
    
    static void DeleteTask(TaskRepository repo)
    {
        var tasks = repo.GetAll().ToList();
        if (!tasks.Any())
        {
            W("No tasks found.", ConsoleColor.Red);
            return;
        }

        foreach (var t in tasks)
        {
            Console.Write($"[{t.Id}] ");
            W($"{t.Title}", ConsoleColor.White);
        }

        W("\nEnter task ID to delete (eg: 1 2 3, 1-3 or *): ", ConsoleColor.DarkGray);
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
            W("No input provided.", ConsoleColor.Red);
            return;
        }

        W("\nDelete selected task(s)? (Y/n): ", ConsoleColor.DarkGray);
        var confirm = Console.ReadKey().Key;
        if (confirm != ConsoleKey.Y && confirm != ConsoleKey.Enter)
        {
            W("\nTask(s) not deleted", ConsoleColor.Red);
            return;
        }

        var allIds = tasks.Select(t => t.Id).ToList();
        var idsToDelete = new List<int>();

        if (input == "*")
        {
            idsToDelete = allIds;
        }
        else
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (part.Contains('-'))
                {
                    var range = part.Split('-');
                    if (range.Length == 2 &&
                        int.TryParse(range[0], out int start) &&
                        int.TryParse(range[1], out int end))
                    {
                        for (int i = start; i <= end; i++)
                            if (allIds.Contains(i)) idsToDelete.Add(i);
                    }
                }
                else if (int.TryParse(part, out int id) && allIds.Contains(id))
                {
                    idsToDelete.Add(id);
                }
            }
        }

        if (!idsToDelete.Any())
        {
            W("No valid tasks selected.", ConsoleColor.Red);
            return;
        }

        foreach (var id in idsToDelete)
            repo.Delete(id);

        W("Task(s) deleted!", ConsoleColor.Green);
    }
    
    static void SetupExitHandler()
    {
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            Console.Clear();
            Console.WriteLine("ctrl + c");
            Console.CursorVisible = true;
            Environment.Exit(0);
        };
    }
}

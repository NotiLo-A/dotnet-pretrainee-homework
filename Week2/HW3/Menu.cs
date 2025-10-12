namespace HW3;

public struct Menu
{
    public string[] Options { get; }
    private int _selectedIndex;

    public Menu(params string[] options)
    {
        Options = options;
        _selectedIndex = 0;
    }

    public int Run()
    {
        ConsoleKey key;
        do
        {
            Draw();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                _selectedIndex = (_selectedIndex - 1 + Options.Length) % Options.Length;
            else if (key == ConsoleKey.DownArrow)
                _selectedIndex = (_selectedIndex + 1) % Options.Length;

        } while (key != ConsoleKey.Enter);

        return _selectedIndex;
    }
    
    private void Draw()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[  Task Manager  ]\n");
        Console.ResetColor();

        for (int i = 0; i < Options.Length; i++)
        {
            if (i == _selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"> {Options[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {Options[i]}");
            }
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nUse ↑ ↓ to select, Enter to continue");
        Console.ResetColor();
    }
}
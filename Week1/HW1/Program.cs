namespace HW1;

class Program
{
    static void Main(string[] args)
    {
        (bool ok, int value, string err) GetNumber()
        {
            string input = Console.ReadLine();
            return int.TryParse(input, out int parsedValue)
                ? (true, parsedValue, null) 
                : (false, 0, $"Invalid input: \"{input}\"");
        }
        
        (bool ok, char op, string error) GetOperator()
        {
            string input = Console.ReadLine();
            return (input.Length == 1 && "+-*/".Contains(input))
                ? (true, input[0], null)
                : (false, '\0', $"Invalid operator: \"{input}\".");
        }
        
        while (true)
        {
            Console.WriteLine("First number: ");
            var (ok1, val1, err1) = GetNumber();
            if (!ok1) { Console.WriteLine(err1); continue; }

            Console.WriteLine("Operator (+, -, *, /): ");
            var (okOp, oper, opError) = GetOperator();
            if (!okOp) { Console.WriteLine(opError); continue; }

            Console.WriteLine("Second number:");
            var (ok2, val2, err2) = GetNumber();
            if (!ok2) { Console.WriteLine(err2); continue; }

            if (oper == '/' && val2 == 0)
            {
                Console.WriteLine("Cannot divide by 0. -_-\n");
                continue;
            }

            int result = 0;
            switch (oper)
            {
                case '+': result = val1 + val2; break;
                case '-': result = val1 - val2; break;
                case '*': result = val1 * val2; break;
                case '/': result = val1 / val2; break;
            }

            Console.WriteLine($"{val1} {oper} {val2} = {result}");

            Console.WriteLine("Again? (Y/n): ");
            string again = Console.ReadLine();
            if (again.ToLower() == "n") break;
        }
    }
}

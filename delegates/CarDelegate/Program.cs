using Car;

namespace CarDelegate;

class Program
{
    static void DisplayMessage (string msg, ConsoleColor txtColor, int printCount)
    {
        ConsoleColor previous = Console.ForegroundColor;
        Console.ForegroundColor = txtColor;

        for (int i = 0; i < printCount; i ++)
        {
            Console.WriteLine(msg);
        }
        Console.ForegroundColor = previous;
    }

    static int Add(int x, int y)
    {
        return x + y;
    }
    static string SumToString(int x, int y)
    {
        return (x+ y).ToString();
    }

    static void Main(string[] args)
    {
        Console.WriteLine("****** Method Group Conversion ******\n");
        Car.Car cl = new Car.Car();
        cl.RegisterWithCarEngine(CallMeHere);
        Console.WriteLine("***** Speeding up *****\n");
        for (int i = 0; i<6; i++)
        {
            cl.Accelarate(20);
            cl.UnregisterWithCarEngine(CallMeHere);
        }
        for (int i = 0; i < 6; i++)
        {
            cl.Accelarate(20);
        }

        Console.WriteLine("***** Fun with Action and Func *****\n");

        Action<string, ConsoleColor, int>actionTarget = 
            new Action<string, ConsoleColor, int>(DisplayMessage);
        actionTarget("Action message!", ConsoleColor.Yellow, 5);

        Func<int, int, int> funcTarget = new Func<int, int, int>(Add);
        int result = funcTarget.Invoke(40, 40);
        Console.WriteLine("40 + 40 = {0}", result);

        Func<int, int, string> funcTarget2 = new Func<int, int, string>(SumToString);
        string sum = funcTarget2(90, 300);
        Console.WriteLine("90 + 300 = {0}", sum);
    }

    static void CallMeHere(string msg)
    {
               Console.WriteLine("=> Message from car: {0}", msg);
    }
}

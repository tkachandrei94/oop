public interface ITask
{
    void Run();
}

public class Task1 : ITask
{
    public void Run()
    {
        int number = 7; // Здесь должно быть число студента (последняя цифра)
        int[] numbers = { 5, 12, 8 }; // Пример данных
        
        var filteredNumbers = numbers.Where(n => n >= 1 && n <= (10 + number)).ToList();

        Console.WriteLine("========================\n");
        Console.WriteLine("Numbers in range [1, " + (10 + number) + "]: " + string.Join(", ", filteredNumbers));
    }
}

public class Task2 : ITask
{
    public void Run()
    {
        double a = 3, b = 4, c = 5;

        if (IsValidTriangle(a, b, c))
        {
            double perimeter = a + b + c;
            double s = perimeter / 2;
            double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            
            Console.WriteLine($"Perimeter: {perimeter}, Area: {area}");
            Console.WriteLine("Type: " + GetTriangleType(a, b, c));
        }
        else
        {
            Console.WriteLine("Invalid triangle");
        }
    }

    private bool IsValidTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    private string GetTriangleType(double a, double b, double c)
    {
        if (a == b && b == c) return "Equilateral";
        if (a == b || b == c || a == c) return "Isosceles";
        return "Scalene";
    }
}

public class Task3 : ITask
{
    public void Run()
    {
        int number = 7; // Последняя цифра номера
        int[] array = new int[10 + number];
        Random rand = new Random();
        
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(-100, 100);
        }

        Console.WriteLine("Array: " + string.Join(", ", array));
        Console.WriteLine("Min: " + array.Min() + ", Max: " + array.Max());
    }
}

public class Task4 : ITask
{
    public void Run()
    {
        int number = 7; // Последняя цифра номера
        int[] array = new int[10 + number];
        Random rand = new Random();
        
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(-100, 100);
        }

        Console.WriteLine("Array: " + string.Join(", ", array));

        int M = 15; // Заданное число M
        var filteredArray = array.Where(x => Math.Abs(x) > M).ToArray();

        Console.WriteLine($"M = {M}");
        Console.WriteLine("Filtered Array: " + string.Join(", ", filteredArray));
    }
}

public class TaskController
{
    private readonly List<ITask> _tasks;

    public TaskController()
    {
        _tasks = new List<ITask>
        {
            new Task1(),
            new Task2(),
            new Task3(),
            new Task4()
        };
    }

    public void RunAll()
    {
        foreach (var task in _tasks)
        {
            task.Run();
            Console.WriteLine("---------------------");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var controller = new TaskController();
        controller.RunAll();
    }
}


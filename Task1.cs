using System;
using System.Linq;

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

using System;
using System.Linq;

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

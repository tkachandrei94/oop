using System;
using System.Linq;

public class Task3 : ITask
{
    public void Run()
    {
        int number = 7;
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

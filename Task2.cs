using System;

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

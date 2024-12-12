using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Класс модели Студента
public class Student
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<string> Subjects { get; set; } = new();
}

public class Program
{
    public static void Main()
    {
        // 1. Создаем список студентов
        var students = new List<Student>
        {
            new Student { Name = "Alice", Age = 20, Subjects = new List<string>{"Math", "Physics"} },
            new Student { Name = "Bob", Age = 22, Subjects = new List<string>{"History", "Literature"} }
        };

        string filePath = "students.json";

        // 2. Сериализуем в JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(students, jsonOptions);

        // Сохраняем в файл
        File.WriteAllText(filePath, json);
        Console.WriteLine("Сериализован список студентов в файл students.json");

        // 3. Десериализуем обратно
        string jsonFromFile = File.ReadAllText(filePath);
        var loadedStudents = JsonSerializer.Deserialize<List<Student>>(jsonFromFile);

        Console.WriteLine("Список студентов после десериализации:");
        PrintStudents(loadedStudents);

        // 4. Допустим, добавим нового студента
        loadedStudents?.Add(new Student { Name = "Charlie", Age = 19, Subjects = new List<string>{"Biology"} });

        // Или обновим данные одного из существующих студентов
        if (loadedStudents != null && loadedStudents.Count > 0)
        {
            loadedStudents[0].Age = 21; // Обновили возраст Alice
        }

        // Повторно сериализуем
        string updatedJson = JsonSerializer.Serialize(loadedStudents, jsonOptions);
        File.WriteAllText(filePath, updatedJson);

        // Снова загружаем из файла и проверяем изменения
        var reloadedStudents = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath));
        Console.WriteLine("Список студентов после обновления и повторной сериализации:");
        PrintStudents(reloadedStudents);
    }

    private static void PrintStudents(List<Student>? students)
    {
        if (students == null) return;

        foreach (var s in students)
        {
            Console.WriteLine($"Name: {s.Name}, Age: {s.Age}, Subjects: {string.Join(", ", s.Subjects)}");
        }
    }
}

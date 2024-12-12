using System;
using System.Collections.Generic;

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

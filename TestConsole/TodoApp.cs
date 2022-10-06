using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole;
public class TodoApp
{
    private const int TableWidth = 83;

    private Dictionary<int, TodoItem> _todoItems = new()
    {
        [1] = new()
        {
            Id = 1,
            Description = "Sort life out",
            CompleteBy = DateTime.Now
        },
        [2] = new()
        {
            Id = 2,
            Description = "Do washing up",
            CompleteBy = DateTime.Now.AddDays(-2),
            IsComplete = true
        },
        [3] = new()
        {
            Id = 3,
            Description = "Walk dog",
            CompleteBy = DateTime.Now.AddHours(1)
        }
    };
    private int _lastId = 3;
    private void Create()
    {
        Console.WriteLine();
        Console.WriteLine("======================================Create======================================");

        var description = ConsoleHelper.GetInput<string>("Please enter description:", "Description cannot be empty", validateFunc: x => !string.IsNullOrEmpty(x));
        var completeByDate = ConsoleHelper.GetInput<DateTime>("Please enter complete by date in the following format – YYYY-MM-dd or press ENTER to skip:", isOptional: true);

        var todoItem = new TodoItem
        {
            Id = ++_lastId,
            Description = description,
            CompleteBy = completeByDate == default ? null : completeByDate
        };

        _todoItems.Add(todoItem.Id, todoItem);
        Console.WriteLine($"Successfully created '{description}'");
        Menu();
    }

    public void Menu()
    {
        Console.WriteLine();
        Console.WriteLine("======================================MENU=======================================");
        Console.WriteLine();
        Console.WriteLine("List all items - l");
        Console.WriteLine();
        Console.WriteLine("Create item - c");
        Console.WriteLine();
        Console.WriteLine();

        var option = ConsoleHelper.GetSelection("Please select an option from above or press [x] to exit", new() { "l", "c", "x" });

        switch (option.ToLower())
        {
            case "c":
                Create();
                break;
            case "x":
                return;
            case "l":
                ListItems();
                break;
        }
    }

    private void ListItems()
    {
        Console.WriteLine();
        Console.WriteLine("======================================All Items=====================================");

        PrintTodoItems();
        Console.WriteLine();
        Console.WriteLine("View item - v");
        Console.WriteLine();
        Console.WriteLine("Main menu - m");
        Console.WriteLine();

        var option = ConsoleHelper.GetSelection("Please select an option from above or press [x] to exit", new() { "v", "m", "x" });

        switch (option.ToLower())
        {
            case "v":
                View();
                break;
            case "x":
                return;
            case "m":
                Menu();
                break;
        }
    }

    private void View()
    {
        Console.WriteLine();
        Console.WriteLine("======================================View========================================");

        var id = ConsoleHelper.GetInput<int>(
            "Please enter the id of the item you wish to view:",
            "Could not find item",
            validateFunc: x => _todoItems.ContainsKey(x));

        var todo = _todoItems[id];

        Console.WriteLine("You have selected:");
        PrintTodoItems(new() { todo });

        Console.WriteLine();
        Console.WriteLine("Edit item - e");
        Console.WriteLine();
        Console.WriteLine("Delete item - d");
        Console.WriteLine();
        Console.WriteLine("Main menu - m");
        Console.WriteLine();

        var option = ConsoleHelper.GetSelection("Please select an option from above or press [x] to exit", new() { "e", "d", "m", "x" });

        switch (option.ToLower())
        {
            case "e":
                Edit(id);
                break; 
            case "d":
                Delete(id);
                break;
            case "x":
                return;
            case "m":
                Menu();
                break;
        }

    }

    private void Edit(int id)
    {
        Console.WriteLine();
        Console.WriteLine("======================================Edit========================================");
    }

    public void Delete(int id)
    {
        Console.WriteLine();
        Console.WriteLine("======================================Delete=======================================");
    }

    private void PrintTodoItems(List<TodoItem> items = null)
    {
        PrintLine();
        PrintRow("Id", "Description", "Complete By", "Completed");
        PrintLine();

        foreach (var todo in items ?? _todoItems.Values.ToList())
        {
            var completeBy = todo.CompleteBy?.ToString("d MMM yyyy") ?? "-";
            var completed = todo.IsComplete ? "Yes" : "No";
            PrintRow(todo.Id.ToString(), todo.Description, completeBy, completed);
        }

        PrintLine();
    }

    private static void PrintLine()
    {
        Console.WriteLine(new string('-', TableWidth));
    }

    private static void PrintRow(params string[] columns)
    {
        var width = (TableWidth - columns.Length) / columns.Length;
        var row = "|";

        foreach (var column in columns)
        {
            row += AlignCentre(column, width) + "|";
        }

        Console.WriteLine(row);
    }

    private static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }

        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}

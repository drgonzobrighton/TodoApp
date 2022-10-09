using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Core;
using TodoApp.Core.Models;

namespace TodoApp.Console;
public class TodoApp
{
    private readonly ITodoService _todoService;
    private const int TableWidth = 83;


    public TodoApp(ITodoService todoService)
    {
        _todoService = todoService;
    }


    public async Task Run()
    {
        Menu();
    }

    private void Create()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("======================================Create======================================");
        string description;

        while (true)
        {
            description = ConsoleHelper.GetInput<string>("Please enter description:", "Description cannot be empty");
            var completeByDate = ConsoleHelper.GetInput<DateTime>("Please enter complete by date in the following format – YYYY-MM-dd or press ENTER to skip:", isOptional: true);

            var result = _todoService.Create(description, completeByDate == default ? null : completeByDate);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ConsoleHelper.WriteError(error);
                }

                continue;
            }

            break;
        }

        System.Console.WriteLine();
        System.Console.WriteLine($"Successfully created '{description}'");
        Menu();
    }

    private void Menu()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("======================================MENU=======================================");
        System.Console.WriteLine();
        System.Console.WriteLine("List all items - l");
        System.Console.WriteLine();
        System.Console.WriteLine("Create item - c");
        System.Console.WriteLine();
        System.Console.WriteLine();

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
        System.Console.WriteLine();
        System.Console.WriteLine("======================================All Items=====================================");

        PrintTodoItems();
        System.Console.WriteLine();
        System.Console.WriteLine("View item - v");
        System.Console.WriteLine();
        System.Console.WriteLine("Main menu - m");
        System.Console.WriteLine();

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
        System.Console.WriteLine();
        System.Console.WriteLine("======================================View========================================");

        TodoItem todo = null;

        while (todo is null)
        {
            var id = ConsoleHelper.GetInput<int>("Please enter the id of the item you wish to view:");
            todo = _todoService.GetById(id);

            if (todo is null)
            {
                ConsoleHelper.WriteError($"Could not find item with id {id}");
            }
        }

        
        System.Console.WriteLine();
        System.Console.WriteLine("You have selected:");
        PrintTodoItems(new() { todo });

        System.Console.WriteLine();
        System.Console.WriteLine("Edit item - e");
        System.Console.WriteLine();
        System.Console.WriteLine("Delete item - d");
        System.Console.WriteLine();
        System.Console.WriteLine("Main menu - m");
        System.Console.WriteLine();

        var option = ConsoleHelper.GetSelection("Please select an option from above or press [x] to exit", new() { "e", "d", "m", "x" });

        switch (option.ToLower())
        {
            case "e":
                Edit(todo.Id);
                break;
            case "d":
                Delete(todo.Id);
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
        System.Console.WriteLine();
        System.Console.WriteLine("======================================Edit========================================");
    }

    public void Delete(int id)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("======================================Delete=======================================");
    }

    private void PrintTodoItems(List<TodoItem> items = null)
    {
        PrintLine();
        PrintRow("Id", "Description", "Complete By", "Completed");
        PrintLine();

        foreach (var todo in items ?? _todoService.GetAll())
        {
            var completeBy = todo.CompleteBy?.ToString("d MMM yyyy") ?? "-";
            var completed = todo.IsComplete ? "Yes" : "No";
            PrintRow(todo.Id.ToString(), todo.Description, completeBy, completed);
        }

        PrintLine();
    }

    private static void PrintLine()
    {
        System.Console.WriteLine(new string('-', TableWidth));
    }

    private static void PrintRow(params string[] columns)
    {
        var width = (TableWidth - columns.Length) / columns.Length;
        var row = "|";

        foreach (var column in columns)
        {
            row += AlignCentre(column, width) + "|";
        }

        System.Console.WriteLine(row);
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

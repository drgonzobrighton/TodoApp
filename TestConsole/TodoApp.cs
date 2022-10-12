using System;
using System.Collections.Generic;
using TodoApp.Core.Models;
using TodoApp.Services;
using static TodoApp.Console.ConsoleHelper;

namespace TodoApp.Console;
public class TodoApp
{
    private readonly ITodoService _todoService;
    private const int TableWidth = 83;

    public TodoApp(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public void Run()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        MenuHeader("Menu");

        var menu = new ConsoleMenu(new()
        {
            new("List all items", ListAll),
            new("Create item", Create),
        });

        menu.Show();
    }

    private void Create()
    {
        MenuHeader("Create");
        string description;

        while (true)
        {
            description = GetInput<string>("Please enter description:", "Description cannot be empty", validateFunc: x => !string.IsNullOrEmpty(x));
            var completeByDate = GetInput<DateTime?>("Please enter complete by date in the following format – YYYY-MM-dd or press ENTER to skip:", isOptional: true);

            var result = _todoService.Create(description, completeByDate);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    WriteError(error);
                }

                continue;
            }

            break;
        }

        System.Console.WriteLine();
        System.Console.WriteLine($"Successfully created '{description}'");
        MainMenu();
    }

    private void ListAll()
    {
        MenuHeader("All items");

        PrintTodoItems();

        var menu = new ConsoleMenu(new()
        {
            new("View item", View),
            new("Main menu", MainMenu),
        });

        menu.Show();
    }

    private void View()
    {
        MenuHeader("View");

        var todo = GetById();

        System.Console.WriteLine();
        System.Console.WriteLine("You have selected:");

        PrintTodoItems(new() { todo });

        var menu = new ConsoleMenu(new()
        {
            new("Edit item", () => Edit(todo.Id)),
            new("Delete item", () => Delete(todo.Id)),
            new("Main menu", MainMenu),
        });

        menu.Show();
    }

    private void Edit(int id)
    {
        MenuHeader("Edit");

        var todo = GetById(id);
    }

    private void Delete(int id)
    {
        MenuHeader("Delete");

        var todo = GetById(id);
    }

    private TodoItem GetById(int? id = null)
    {
        TodoItem todo = null;

        while (todo is null)
        {
            id ??= GetInput<int>("Please enter the id of the item you wish to view:");

            todo = _todoService.GetById(id.Value);

            if (todo is null)
            {
                WriteError($"Could not find item with id {id}");
                id = null;
            }
        }

        return todo;
    }

    private static void MenuHeader(string menuName)
    {
        var length = (TableWidth - menuName.Length) / 2;
        System.Console.WriteLine();
        System.Console.WriteLine($"{new string('=', length)}{menuName}{new string('=', length)}");
    }

    private void PrintTodoItems(List<TodoItem> items = null)
    {
        items ??= _todoService.GetAll();
        PrintLine();
        PrintRow("Id", "Description", "Complete By", "Completed");
        PrintLine();

        foreach (var todo in items)
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

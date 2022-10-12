using System;
using System.Collections.Generic;
using System.Linq;
using static TodoApp.Console.ConsoleHelper;

namespace TodoApp.Console;
public class ConsoleMenu
{
    private Dictionary<string, ConsoleMenuItem> _menuItems;

    public ConsoleMenu(List<ConsoleMenuItem> menuItems)
    {
        _menuItems = menuItems.ToDictionary(x => x.Key);
    }

    public void Show()
    {
        System.Console.WriteLine();

        foreach (var (_, menuItem) in _menuItems)
        {
            System.Console.WriteLine($"{menuItem.Name} - {menuItem.Key}");
            System.Console.WriteLine();
        }

        System.Console.WriteLine();

        var option = GetSelection("Please select an option from above or press [x] to exit", new(_menuItems.Select(x => x.Key)) { "x" });

        if (option == "x")
        {
            return;
        }

        _menuItems[option].ShowScreen.Invoke();
    }
}

public class ConsoleMenuItem
{
    public string Name { get; }
    public Action ShowScreen { get; }
    public string Key { get; }

    public ConsoleMenuItem(string name, Action showScreen, string key = null)
    {
        Name = name;
        ShowScreen = showScreen;
        Key = string.IsNullOrEmpty(key) ? name.First().ToString().ToLower() : key.ToLower();
    }
}

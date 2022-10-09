using System;
using System.Collections.Generic;
using System.Globalization;

namespace TodoApp.Console;
public static class ConsoleHelper
{
    public static T GetInput<T>(string prompt, string validationErrorMessage = "", bool isOptional = false, Func<T, bool> validateFunc = null)
    {
        T result = default;

        while (true)
        {
            System.Console.WriteLine(prompt);

            var userInput = System.Console.ReadLine();

            if (string.IsNullOrEmpty(userInput) && isOptional)
            {
                return default;
            }

            try
            {
                result = (T)Convert.ChangeType(userInput, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                WriteError($"Input is not valid, expected a {GetTypeName(result)}");
                continue;
            }

            if (validateFunc is not null && !validateFunc(result))
            {
                WriteError(validationErrorMessage);
                continue;
            }

            break;
        }

        return result;
    }

    public static string GetSelection(string prompt, List<string> values)
    {
        string selection;

        while (true)
        {
            System.Console.WriteLine(prompt);

            var userInput = System.Console.ReadLine();

            if (!values.Contains(userInput!.ToLower()))
            {
                WriteError("Please choose a valid option");
                continue;
            }

            selection = userInput;
            break;
        }

        return selection;
    }

    private static string GetTypeName<T>(T o)
    {
        return o switch
        {
            int or double or long or float => "number",
            DateTime => "date",
            _ => o.GetType().Name
        };
    }

    public static void WriteError(string message)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(message);
        System.Console.ForegroundColor = ConsoleColor.White;
    }
}

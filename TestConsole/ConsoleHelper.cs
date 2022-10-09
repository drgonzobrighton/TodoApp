using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Console;

namespace TodoApp.Console;
public static class ConsoleHelper
{
    public static T GetInput<T>(string prompt, string validationErrorMessage = "", bool isOptional = false, Func<T, bool> validateFunc = null)
    {
        T result = default;

        while (true)
        {
            WriteLine(prompt);
            var userInput = ReadLine();

            if (string.IsNullOrEmpty(userInput) && isOptional)
            {
                return default;
            }

            try
            {
                var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                result = (T)Convert.ChangeType(userInput, type, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                WriteError($"Input is not valid, expected a {GetTypeName(result)}");
                continue;
            }

            if (validateFunc is not null && !validateFunc.Invoke(result))
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
            WriteLine(prompt);
            var userInput = ReadLine();

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
        ForegroundColor = ConsoleColor.Red;
        WriteLine(message);
        ForegroundColor = ConsoleColor.White;
    }
}

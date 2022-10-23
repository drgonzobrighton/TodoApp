using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.DataAccess;

namespace TodoApp.Services;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddToDoService(this IServiceCollection collection)
    {
        collection.AddSingleton<ITodoService, TodoService>();
        //collection.AddSingleton<ITodoRepository, DictionaryTodoRepository>();
        collection.AddTransient<ITodoRepository, EfTodoRepository>();
        collection.AddTransient<TodoContext>();
        return collection;
    }
}

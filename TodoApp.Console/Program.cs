using Microsoft.Extensions.DependencyInjection;
using TodoApp.DataAccess;
using TodoApp.Services;

var services = new ServiceCollection();

services.AddSingleton<TodoApp.Console.TodoApp>();
services.AddSingleton<ITodoService, TodoService>();
//services.AddSingleton<ITodoRepository, DictionaryTodoRepository>();
services.AddTransient<ITodoRepository, EfTodoRepository>();
services.AddTransient<TodoContext>();

var serviceProvider = services.BuildServiceProvider();

serviceProvider.GetService<TodoApp.Console.TodoApp>()!.Run();


//test comment


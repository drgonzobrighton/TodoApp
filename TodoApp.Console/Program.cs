using Microsoft.Extensions.DependencyInjection;
using TodoApp.Services;

var services = new ServiceCollection();

services
    .AddSingleton<TodoApp.Console.TodoApp>()
    .AddToDoService();

var serviceProvider = services.BuildServiceProvider();

serviceProvider.GetService<TodoApp.Console.TodoApp>()!.Run();
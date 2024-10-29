namespace Preezie.WebAPI.Providers;

using Preezie.Application.Interfaces;
using Preezie.Application.Services;

public static class TodoItemServiceProvider
{
    public static IServiceCollection AddTodoItemServices(this IServiceCollection services)
    {
        services.AddSingleton<IReadOnlyTodoItemService, TodoItemService>();
        services.AddSingleton<ITodoItemService, TodoItemService>();
        return services;
    }
}

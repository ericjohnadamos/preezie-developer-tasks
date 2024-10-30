namespace Preezie.WebAPI.Providers;

using Preezie.Application.Interfaces;
using Preezie.Application.Services;

public static class TodoItemServiceProvider
{
    public static IServiceCollection AddTodoItemServices(this IServiceCollection services)
    {
        services.AddSingleton<TodoItemService>();
        services.AddSingleton<ITodoItemService>(sp => sp.GetRequiredService<TodoItemService>());
        services.AddSingleton<IReadOnlyTodoItemService>(sp => sp.GetRequiredService<TodoItemService>());

        return services;
    }
}

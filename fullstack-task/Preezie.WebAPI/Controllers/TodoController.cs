using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    public static List<TodoItem> todos = new List<TodoItem>();
    public int nextId = 1;

    [HttpGet]
    public IEnumerable<TodoItem> GetTodos()
    {
        return todos;
    }

    [HttpPost]
    public void AddTodo(TodoItem item)
    {
        item.Id = nextId;
        nextId++;
        todos.Add(item);
    }

    [HttpPut("{id}")]
    public void UpdateTodo(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
        }
    }

    [HttpDelete]
    public void DeleteTodo(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo != null)
        {
            todos.Remove(todo);
        }
    }
}

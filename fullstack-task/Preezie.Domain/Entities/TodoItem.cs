namespace Preezie.Domain.Entities;

public class TodoItem
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }

    private TodoItem() { }

    public static TodoItem CreateWithTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title is required.");

        return new TodoItem
        {
            Title = title,
        };
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new Exception("Title is required.");

        Title = newTitle;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}

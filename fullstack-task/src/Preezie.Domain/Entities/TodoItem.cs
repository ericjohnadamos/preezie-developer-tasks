namespace Preezie.Domain.Entities;

public record TodoItem
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
    public bool IsDeleted { get; init; }

    private TodoItem() { }

    public static TodoItem CreateWithIdAndTitle(int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.");

        return new TodoItem
        {
            Id = id,
            Title = title,
        };
    }

    public TodoItem WithUpdatedTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title is required.");

        return this with
        {
            Title = newTitle,
        };
    }

    public TodoItem WithDeletedStatus()
    {
        return this with
        {
            IsDeleted = true,
        };
    }
}

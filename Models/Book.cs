namespace LibraryManagement.Api.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int CategoryId { get; set; }

    public int TotalCount { get; set; }

    public int AvailableCount { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
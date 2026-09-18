namespace LibraryManagement.Api.DTOs;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int CategoryId { get; set; }

    public int TotalCount { get; set; }
}
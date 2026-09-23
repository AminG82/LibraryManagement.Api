namespace LibraryManagement.Api.DTOs;

public class BorrowingDto
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;

    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;

    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
}
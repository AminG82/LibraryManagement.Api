namespace LibraryManagement.Api.Models;

public class Borrowing
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int MemberId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }

    public Book Book { get; set; } = null!;

    public Member Member { get; set; } = null!;
}
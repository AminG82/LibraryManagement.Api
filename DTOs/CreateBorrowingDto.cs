namespace LibraryManagement.Api.DTOs;

public class CreateBorrowingDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
}
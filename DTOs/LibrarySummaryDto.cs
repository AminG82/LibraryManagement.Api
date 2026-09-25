namespace LibraryManagement.Api.DTOs;

public class LibrarySummaryDto
{
    public int TotalBooks { get; set; }
    public int AvailableBooks { get; set; }
    public int BorrowedBooks { get; set; }
    public int TotalMembers { get; set; }
}
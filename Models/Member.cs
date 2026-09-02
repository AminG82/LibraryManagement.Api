namespace LibraryManagement.Api.Models;

public class Member
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string NationalCode { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public DateTime RegisterDate { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
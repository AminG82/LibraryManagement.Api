namespace LibraryManagement.Api.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime RegisterDate { get; set; }
}
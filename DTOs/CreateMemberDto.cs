namespace LibraryManagement.Api.DTOs;

public class CreateMemberDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string? Phone { get; set; }
}
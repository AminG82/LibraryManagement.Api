using LibraryManagement.Api.Data;
using LibraryManagement.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public ReportsController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/Reports/summary
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var totalBooks = await _context.Books
            .SumAsync(b => b.TotalCount);

        var availableBooks = await _context.Books
            .SumAsync(b => b.AvailableCount);

        var totalMembers = await _context.Members
            .CountAsync();

        var borrowedBooks = totalBooks - availableBooks;

        var result = new LibrarySummaryDto
        {
            TotalBooks = totalBooks,
            AvailableBooks = availableBooks,
            BorrowedBooks = borrowedBooks,
            TotalMembers = totalMembers
        };

        return Ok(result);
    }
}
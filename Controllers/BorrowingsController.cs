using LibraryManagement.Api.Data;
using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingsController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public BorrowingsController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/Borrowings
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var borrowings = await _context.Borrowings
            .Include(b => b.Book)
            .Include(b => b.Member)
            .Select(b => new BorrowingDto
            {
                Id = b.Id,

                BookId = b.BookId,
                BookTitle = b.Book.Title,

                MemberId = b.MemberId,
                MemberName = b.Member.FirstName + " " + b.Member.LastName,

                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate,
                IsReturned = b.IsReturned
            })
            .ToListAsync();

        return Ok(borrowings);
    }

    // GET: api/Borrowings/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var borrowing = await _context.Borrowings
            .Include(b => b.Book)
            .Include(b => b.Member)
            .Where(b => b.Id == id)
            .Select(b => new BorrowingDto
            {
                Id = b.Id,

                BookId = b.BookId,
                BookTitle = b.Book.Title,

                MemberId = b.MemberId,
                MemberName = b.Member.FirstName + " " + b.Member.LastName,

                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate,
                IsReturned = b.IsReturned
            })
            .FirstOrDefaultAsync();

        if (borrowing == null)
            return NotFound();

        return Ok(borrowing);
    }

    // POST: api/Borrowings
    [HttpPost]
    public async Task<IActionResult> Create(CreateBorrowingDto dto)
    {
        // Check book
        var book = await _context.Books.FindAsync(dto.BookId);

        if (book == null)
            return NotFound("Book not found.");

        // Check member
        var member = await _context.Members.FindAsync(dto.MemberId);

        if (member == null)
            return NotFound("Member not found.");

        // Check availability
        if (book.AvailableCount <= 0)
            return BadRequest("No available copies of this book.");

        // Check if this member already has this book
        var alreadyBorrowed = await _context.Borrowings
            .AnyAsync(b =>
                b.BookId == dto.BookId &&
                b.MemberId == dto.MemberId &&
                !b.IsReturned);

        if (alreadyBorrowed)
            return BadRequest(
                "This member has already borrowed this book."
            );

        // Create borrowing
        var borrowing = new Borrowing
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowDate = DateTime.UtcNow,
            IsReturned = false
        };

        // Decrease available copies
        book.AvailableCount--;

        _context.Borrowings.Add(borrowing);

        await _context.SaveChangesAsync();

        var result = new BorrowingDto
        {
            Id = borrowing.Id,
            BookId = borrowing.BookId,
            BookTitle = book.Title,
            MemberId = borrowing.MemberId,
            MemberName = member.FirstName + " " + member.LastName,
            BorrowDate = borrowing.BorrowDate,
            ReturnDate = borrowing.ReturnDate,
            IsReturned = borrowing.IsReturned
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = borrowing.Id },
            result
            );
    }

    // PUT: api/Borrowings/1/return
    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var borrowing = await _context.Borrowings
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (borrowing == null)
            return NotFound();

        // Already returned
        if (borrowing.IsReturned)
            return BadRequest("This book has already been returned.");

        // Mark as returned
        borrowing.IsReturned = true;
        borrowing.ReturnDate = DateTime.UtcNow;

        // Increase available copies
        borrowing.Book.AvailableCount++;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Borrowings/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);

        if (borrowing == null)
            return NotFound();

        // Do not allow deleting active borrowing
        if (!borrowing.IsReturned)
            return BadRequest(
                "An active borrowing cannot be deleted. Return the book first."
            );

        _context.Borrowings.Remove(borrowing);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
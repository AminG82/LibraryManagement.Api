using LibraryManagement.Api.Data;
using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public BooksController(LibraryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
            .Include(b => b.Category)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name,
                TotalCount = b.TotalCount,
                AvailableCount = b.AvailableCount
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _context.Books
            .Include(b => b.Category)
            .Where(b => b.Id == id)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name,
                TotalCount = b.TotalCount,
                AvailableCount = b.AvailableCount
            })
            .FirstOrDefaultAsync();

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("Category does not exist.");

        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            CategoryId = dto.CategoryId,
            TotalCount = dto.TotalCount,
            AvailableCount = dto.TotalCount
        };

        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = book.Id },
            book
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("Category does not exist.");

        var borrowedCount = book.TotalCount - book.AvailableCount;

        if (dto.TotalCount < borrowedCount)
            return BadRequest(
                "Total count cannot be less than the number of borrowed copies."
            );

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.ISBN = dto.ISBN;
        book.CategoryId = dto.CategoryId;
        book.TotalCount = dto.TotalCount;
        book.AvailableCount = dto.TotalCount - borrowedCount;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
using LibraryManagement.Api.Data;
using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public MembersController(LibraryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _context.Members
            .Select(m => new MemberDto
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                NationalCode = m.NationalCode,
                Phone = m.Phone,
                RegisterDate = m.RegisterDate
            })
            .ToListAsync();

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _context.Members
            .Where(m => m.Id == id)
            .Select(m => new MemberDto
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                NationalCode = m.NationalCode,
                Phone = m.Phone,
                RegisterDate = m.RegisterDate
            })
            .FirstOrDefaultAsync();

        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberDto dto)
    {
        var nationalCodeExists = await _context.Members
            .AnyAsync(m => m.NationalCode == dto.NationalCode);

        if (nationalCodeExists)
            return BadRequest("A member with this national code already exists.");

        var member = new Member
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            NationalCode = dto.NationalCode,
            Phone = dto.Phone,
            RegisterDate = DateTime.UtcNow
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = member.Id },
            member
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
    {
        var member = await _context.Members.FindAsync(id);

        if (member == null)
            return NotFound();

        var nationalCodeExists = await _context.Members
            .AnyAsync(m =>
                m.NationalCode == dto.NationalCode &&
                m.Id != id);

        if (nationalCodeExists)
            return BadRequest("A member with this national code already exists.");

        member.FirstName = dto.FirstName;
        member.LastName = dto.LastName;
        member.NationalCode = dto.NationalCode;
        member.Phone = dto.Phone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var member = await _context.Members.FindAsync(id);

        if (member == null)
            return NotFound();

        var hasBorrowings = await _context.Borrowings
            .AnyAsync(b => b.MemberId == id);

        if (hasBorrowings)
            return BadRequest(
                "This member cannot be deleted because they have borrowing records."
            );

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();

        return NoContent();

        //Fully Tested!
    }
}
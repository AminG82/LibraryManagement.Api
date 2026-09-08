using LibraryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Borrowing> Borrowings => Set<Borrowing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .ToTable("categories");

        modelBuilder.Entity<Book>()
            .ToTable("books");

        modelBuilder.Entity<Member>()
            .ToTable("members");

        modelBuilder.Entity<Borrowing>()
            .ToTable("borrowings");
    }
}
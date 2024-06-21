using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<BookCategory> BookCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BookCategory>()
				.HasKey(bookCategory => new { bookCategory.BookId, bookCategory.CategoryId });

			modelBuilder.Entity<BookCategory>()
				.HasOne(bookCategory => bookCategory.Book)
				.WithMany(book => book.BookCategories)
				.HasForeignKey(bookCategory => bookCategory.BookId);

			modelBuilder.Entity<BookCategory>()
				.HasOne(bookCategory => bookCategory.Category)
				.WithMany(category => category.BookCategories)
				.HasForeignKey(bookCategory => bookCategory.CategoryId);

			// Seed data
			modelBuilder.Entity<User>().HasData(
				new User { Id = 1, Name = "Jan Kowalski", Email = "kowalski@example.com" },
				new User { Id = 2, Name = "Katarzyna Figura", Email = "Figura@example.com" }
			);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Science Fiction" },
				new Category { Id = 2, Name = "Fantasy" }
			);

			modelBuilder.Entity<Book>().HasData(
				new Book { Id = 1, Title = "Dune", Author = "Frank Herbert", IsAvailable = true },
				new Book { Id = 2, Title = "The Hobbit", Author = "J.R.R. Tolkien", IsAvailable = true }
			);

			modelBuilder.Entity<Review>().HasData(
				new Review { Id = 1, Rating = 5, Comment = "Amazing book", BookId = 1 },
				new Review { Id = 2, Rating = 4, Comment = "Great read.", BookId = 2 }
			);

			modelBuilder.Entity<Order>().HasData(
				new Order { Id = 1, OrderDate = DateTime.Now, UserId = 1 }
			);

			modelBuilder.Entity<OrderDetail>().HasData(
				new OrderDetail { Id = 1, OrderId = 1, BookId = 1, Quantity = 1 }
			);

			modelBuilder.Entity<BookCategory>().HasData(
				new BookCategory { BookId = 1, CategoryId = 1 },
				new BookCategory { BookId = 2, CategoryId = 2 }
			);
		}

	}
}

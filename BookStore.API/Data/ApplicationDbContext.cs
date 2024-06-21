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
		}
	}
}

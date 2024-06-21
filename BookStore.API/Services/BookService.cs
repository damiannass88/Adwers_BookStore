using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Services
{
	public class BookService
	{
		private readonly ApplicationDbContext _context;

		public BookService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
		{
			return await _context.Books
				.Where(b => b.IsAvailable)
				.ToListAsync();
		}

		public async Task<Book> GetBookDetailsAsync(int id)
		{
			return await _context.Books
				.Include(b => b.Reviews)
				.Include(b => b.BookCategories)
				.ThenInclude(bc => bc.Category)
				.FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task<bool> RentBookAsync(int bookId, int userId)
		{
			var book = await _context.Books.FindAsync(bookId);
			if (book == null || !book.IsAvailable)
			{
				return false;
			}

			var order = new Order
			{
				OrderDate = DateTime.Now,
				UserId = userId,
				OrderDetails = new List<OrderDetail>
				{
					new OrderDetail
					{
						BookId = bookId,
						Quantity = 1
					}
				}
			};

			book.IsAvailable = false;
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}

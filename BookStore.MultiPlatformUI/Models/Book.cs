namespace BookStore.MultiPlatformUI.Models;

public class Book
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public bool IsAvailable { get; set; }

	public ICollection<Review> Reviews { get; set; }
	public ICollection<OrderDetail> OrderDetails { get; set; }
	public ICollection<BookCategory> BookCategories { get; set; }
}
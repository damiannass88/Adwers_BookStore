﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.MultiPlatformUI.Models;

namespace BookStore.MultiPlatformUI.Services
{
	public class BookService(HttpClient httpClient)
	{
		public async Task<List<Book>?> GetBooksAsync()
		{
			var response = await httpClient.GetAsync("/api/Books");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Book>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<Book?> GetBookAsync(int id)
		{
			var response = await httpClient.GetAsync($"/api/Books/{id}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<Book>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<bool> AddBookAsync(Book book)
		{
			var content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("/api/Books", content);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateBookAsync(Book book)
		{
			var content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
			var response = await httpClient.PutAsync($"/api/Books/{book.Id}", content);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteBookAsync(int id)
		{
			var response = await httpClient.DeleteAsync($"/api/Books/{id}");

			return response.IsSuccessStatusCode;
		}

		public async Task<List<Book>?> GetAvailableBooksAsync()
		{
			var response = await httpClient.GetAsync("/api/Books/available");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Book>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<bool> RentBookAsync(int bookId, int userId)
		{
			var rentData = new { bookId, userId };
			var content = new StringContent(JsonSerializer.Serialize(rentData), Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("/api/Books/rent", content);

			return response.IsSuccessStatusCode;
		}
	}


}

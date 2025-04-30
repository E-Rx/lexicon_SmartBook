using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SmartBook
{
  public class Library
  {
    private List<Book> books;
    private readonly string jsonFilePath;

    // Constructor
    public Library(string jsonFilePath = "library.json")
    {
      books = new List<Book>();
      this.jsonFilePath = jsonFilePath;
    }

    // Property to access the book collection
    public IReadOnlyList<Book> Books => books.AsReadOnly();

    // Method to add a book
    public bool AddBook(Book book)
    {

      // Check for duplicate ISBN
      if (books.Any(b => b.ISBN == book.ISBN))
      {
        return false;
      }

      books.Add(book);
      return true;
    }

    // Method to remove a book by title (case-insensitive) using LINQ
    public bool RemoveBookByTitle(string title)
    {
      var bookToRemove = books.FirstOrDefault(b =>
          b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

      if (bookToRemove != null)
      {
        books.Remove(bookToRemove);
        return true;
      }

      return false;
    }

    // Method to remove a book by ISBN
    public bool RemoveBookByISBN(string isbn)
    {
      var bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);

      if (bookToRemove != null)
      {
        books.Remove(bookToRemove);
        return true;
      }

      return false;
    }

    // Method to search books by combined criteria using LINQ
    public List<Book> SearchBooks(string searchTerm)
    {
      if (string.IsNullOrWhiteSpace(searchTerm))
        return new List<Book>();

      searchTerm = searchTerm.ToLower();

      return books.Where(b =>
          b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
          b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
          b.ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
          b.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
      ).ToList();
    }

    // Method to toggle a book's availability status by ISBN
    public bool ToggleBookAvailability(string isbn)
    {
      var book = books.FirstOrDefault(b => b.ISBN == isbn);

      if (book != null)
      {
        book.ToggleAvailability();
        return true;
      }

      return false;
    }

    // Method to get all books sorted by title (using LINQ)
    public List<Book> GetAllBooksSortedByTitle()
    {
      return books
          .OrderBy(b => b.Title)
          .ToList();
    }

    // Method to find a book by ISBN
    public Book? GetBookByISBN(string isbn)
    {
      return books.FirstOrDefault(b =>
             b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }

    // Method to save the library to a JSON file
    public bool SaveToJson()
    {
      try
      {
        var options = new JsonSerializerOptions
        {
          WriteIndented = true,
          //special characters
          Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        string jsonString = JsonSerializer.Serialize(books, options);
        File.WriteAllText(jsonFilePath, jsonString);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    // Method to load the library from a JSON file
    public bool LoadFromJson()
    {
      try
      {
        if (File.Exists(jsonFilePath))
        {
          string jsonString = File.ReadAllText(jsonFilePath);
          var loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString);

          if (loadedBooks != null)
          {
            books = loadedBooks;
            return true;
          }
        }
        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }

  }
}

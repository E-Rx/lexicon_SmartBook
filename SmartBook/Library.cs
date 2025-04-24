using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Threading;


namespace SmartBook;

public class Library
{
        private List<Book> _books;
        private readonly string _jsonFilePath;

        // Constructor
        public Library(string jsonFilePath = "library.json")
        {
            _books = new List<Book>();
            _jsonFilePath = jsonFilePath;
        }

        // Property to access the book collection
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        // Method to add a book
        public bool AddBook(Book book)
        {
            // Validate the book
            if (!book.IsValid())
            {
                return false;
            }

            // Check for duplicate ISBN
            if (_books.Any(b => b.ISBN == book.ISBN))
            {
                return false;
            }

            _books.Add(book);
            return true;
        }

        // Method to remove a book by title
        public bool RemoveBookByTitle(string title)
        {
            var bookToRemove = _books.FirstOrDefault(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                return true;
            }

            return false;
        }

        // Method to remove a book by ISBN
        public bool RemoveBookByISBN(string isbn)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.ISBN == isbn);

            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                return true;
            }

            return false;
        }

        // Method to search books by title (using LINQ)
        public List<Book> SearchByTitle(string searchTerm)
        {
            return _books
                .Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Method to search books by author (using LINQ)
        public List<Book> SearchByAuthor(string searchTerm)
        {
            return _books
                .Where(b => b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Method to toggle a book's availability status by ISBN
        public bool ToggleBookAvailability(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);

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
            return _books
                .OrderBy(b => b.Title)
                .ToList();
        }

        // Method to get all available books (using LINQ)
        public List<Book> GetAvailableBooks()
        {
            return _books
                .Where(b => b.IsAvailable)
                .ToList();
        }

        // Method to get all borrowed books (using LINQ)
        public List<Book> GetBorrowedBooks()
        {
            return _books
                .Where(b => !b.IsAvailable)
                .ToList();
        }

        // Method to save the library to a JSON file
        public bool SaveToJson()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(_books, options);
                File.WriteAllText(_jsonFilePath, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Book> SearchBooks(string searchTerm)
{
    if (string.IsNullOrWhiteSpace(searchTerm))
        return new List<Book>();

    searchTerm = searchTerm.ToLower();

    return books.Where(b =>
        b.Title.ToLower().Contains(searchTerm) ||
        b.Author.ToLower().Contains(searchTerm) ||
        b.ISBN.ToLower().Contains(searchTerm) ||
        b.Category.ToLower().Contains(searchTerm)
    ).ToList();
}

        // Method to load the library from a JSON file
        public bool LoadFromJson()
        {
            try
            {
                if (File.Exists(_jsonFilePath))
                {
                    string jsonString = File.ReadAllText(_jsonFilePath);
                    var loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString);

                    if (loadedBooks != null)
                    {
                        _books = loadedBooks;
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

        // Method to find a book by ISBN
        public Book? GetBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }
    }
}
}

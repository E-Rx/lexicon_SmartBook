using System;
using System.Linq;

namespace SmartBook
{
  public class Book
  {
    // properties for the Book class
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }

    // constructor for JSON serialization
    public Book()
    {
      Title = string.Empty;
      Author = string.Empty;
      ISBN = string.Empty;
      Category = string.Empty;
      IsAvailable = true;
    }

    // constructor for the Book class with optional isAvailable parameter
    public Book(string title, string author, string isbn, string category, bool isAvailable = true)
    {

      Title = title;
      Author = author;
      ISBN = isbn;
      Category = category;
      IsAvailable = isAvailable;

      if (!IsValid())
      {
        throw new ArgumentException("All fields must be filled.");
      }
      /* if (!IsValid() || !IsValidISBN(isbn))
      {
        throw new ArgumentException("All fields must be filled and ISBN must valid.");
      } */
    }

/*     private bool IsValidISBN(string isbn)
    {
      if (isbn.Length == 10 || isbn.Length == 13)
      {
        return isbn.All(char.IsDigit);
      }
      return false;
    } */

    public override string ToString()
    {
      string status = IsAvailable ? "Available" : "Borrowed";
      return $"Title: {Title}\nAuthor: {Author}\nISBN: {ISBN}\nCategory: {Category}\nStatus: {status}";
    }

    // method to toggle the availability of the book
    public bool ToggleAvailability()
    {
      IsAvailable = !IsAvailable;
      return IsAvailable;
    }

    // method to check if the book is valid
    public bool IsValid()
    {
      return !string.IsNullOrWhiteSpace(Title) &&
            !string.IsNullOrWhiteSpace(Author) &&
            !string.IsNullOrWhiteSpace(ISBN) &&
            !string.IsNullOrWhiteSpace(Category);
    }
  }
}

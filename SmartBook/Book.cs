using System;
using System.Text.Json;


namespace SmartBook;

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


    // constructor for the Book class
    public Book(string title, string author, string isbn, string category, bool isAvailable)
    {
      Title = title;
      Author = author;
      ISBN = isbn;
      Category = category;
      IsAvailable = true; // default value
    }

    public override string ToString()
    {
      string status = IsAvailable ? "Available" : "Borrowed";
      return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Category: {Category}, IsAvailable: {IsAvailable}";
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

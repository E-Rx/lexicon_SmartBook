// File: Program.cs

using System;

namespace SmartBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(@"
             __   _       __    ___  _____      ___   ___   ___   _
            ( (` | |\/|  / /\  | |_)  | |      | |_) / / \ / / \ | |_/
            _)_) |_|  | /_/--\ |_| \  |_|      |_|_) \_\_/ \_\_/ |_| \
                                                                    ");

            var app = new LibraryApp();
            app.LoadLibrary();

            bool running = true;
            while (running)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine() ?? string.Empty;
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        app.ListAllBooks();
                        break;
                    case "2":
                        app.SearchBooks();
                        break;
                    case "3":
                        app.AddBook();
                        break;
                    case "4":
                        app.RemoveBook();
                        break;
                    case "5":
                        app.SaveLibrary();
                        break;
                    case "0":
                        app.Exit();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void DisplayMainMenu()
        {
            Console.WriteLine("\n===== LIBRARY MANAGEMENT SYSTEM =====");
            Console.WriteLine("\n1. View all books");
            Console.WriteLine("2. Search for books (& see book availability)");
            Console.WriteLine("3. Add a new book");
            Console.WriteLine("4. Remove a book");
            Console.WriteLine("5. Save library to file");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter your choice (0-5): ");
        }
    }
}


/* using System;
using System.Collections.Generic;

namespace SmartBook
{
  class Program
  {
    private static Library library;
    static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      //Console.Clear();
      Console.WriteLine(@"
             __   _       __    ___  _____      ___   ___   ___   _
            ( (` | |\/|  / /\  | |_)  | |      | |_) / / \ / / \ | |_/
            _)_) |_|  | /_/--\ |_| \  |_|      |_|_) \_\_/ \_\_/ |_| \
                                                                    ");

      library = new Library();

      // try to load the library data from Json file
      try
      {
        library.LoadFromJson();
        Console.WriteLine("Library data loaded successfully.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading library data: {ex.Message}");
        Console.WriteLine("No existing library data found or could not be loaded.");
        Console.WriteLine("Starting with an empty library.");
      }

      // Main menu loop
      bool running = true;
      while (running)
      {
        DisplayMainMenu();
        string choice = Console.ReadLine() ?? string.Empty;
        Console.Clear();

        switch (choice)
        {
          case "1":
            ListAllBooks();
            break;
          case "2":
            SearchBooks();
            break;
          case "3":
            AddBook();
            break;
          case "4":
            RemoveBook();
            break;
          case "5":
            SaveLibrary();
            break;
          case "0":
            running = false;
            Console.Clear();
            Console.WriteLine("Thank you for using SmartBook!");

            // Save before exiting
            if (library.SaveToJson())
            {
              Console.WriteLine("Library data saved successfully before exit.");
            }
            else
            {
              Console.WriteLine("Failed to save library data before exit.");
            }
            break;
        }

        // Return to main menu
        if (running && choice != "0")
        {
          Console.WriteLine("\nPress any key to return to the main menu...");
          Console.ReadKey();
          Console.Clear();
        }
      }
    }

    // Display the main menu
    private static void DisplayMainMenu()
    {
      Console.WriteLine("\n===== LIBRARY MANAGEMENT SYSTEM =====");
      Console.WriteLine("\n1. View all books");
      Console.WriteLine("2. Search for books (& see book availability)");
      Console.WriteLine("3. Add a new book");
      Console.WriteLine("4. Remove a book");
      Console.WriteLine("5. Save library to file");
      Console.WriteLine("0. Exit");
      Console.Write("\nEnter your choice (0-5): ");
    }

    // List all books in the library
    private static void ListAllBooks()
    {
      Console.Clear();
      Console.WriteLine("\n===== ALL BOOKS (sorted by title) =====");

      var books = library.GetAllBooksSortedByTitle();

      if (books.Count == 0)
      {
        Console.WriteLine("No books found in the library.");
      }
      else
      {
        DisplayBookList(books);
        Console.WriteLine($"\nTotal books: {books.Count}");
      }
    }

    // Search for books by title, author, ISBN, or category (and display their availability status)
    private static void SearchBooks()
    {
      Console.Clear();
      Console.WriteLine("\n===== SEARCH BOOKS =====");
      Console.Write("Enter search term (title, author, ISBN, or category): ");
      string searchTerm = Console.ReadLine() ?? string.Empty;

      var results = library.SearchBooks(searchTerm);

      if (results.Count == 0)
      {
        Console.WriteLine("No books found matching the search term.");
      }
      else
      {
        DisplayBookList(results);
        Console.WriteLine("-----------------------");
      }
      Console.WriteLine($"\nTotal books found: {results.Count}");
    }

    // Display the list of books with formatting
    private static void DisplayBookList(List<Book> books)
    {
      Console.WriteLine("\n");
      int index = 1;

      // Loop through each book and display its details
      foreach (var book in books)
      {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($" {index}. {book.Title}");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine($"    Author: {book.Author}");
        Console.WriteLine($"    ISBN: {book.ISBN}");
        Console.WriteLine($"    Category: {book.Category}");
        Console.Write($"    Status: ");

        if (book.IsAvailable)
        {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Available");
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Borrowed");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-----------------------");
        index++;
      }
    }
    // Add a book to the library
    private static void AddBook()
    {
      Console.WriteLine("\n===== ADD A BOOK =====");
      Console.Write("Enter book title: ");
      string title = Console.ReadLine() ?? string.Empty;

      Console.Write("Enter book author: ");
      string author = Console.ReadLine() ?? string.Empty;

      Console.Write("Enter book ISBN: ");
      string isbn = Console.ReadLine() ?? string.Empty;

      Console.Write("Enter book category: ");
      string category = Console.ReadLine() ?? string.Empty;

      // Create a new book object with the provided details and add it to the library
      var newBook = new Book(title, author, isbn, category);

      // Check if the book adds successfully
      try
      {
        if (library.AddBook(newBook))
        {
          Console.WriteLine("Book added successfully!");
        }
        else
        {
          Console.WriteLine("Failed to add book. Make sure all fields are filled and ISBN is unique.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error adding book: {ex.Message}");
      }
    }

    // Remove a book from the library by title or ISBN
    private static void RemoveBook()
    {
      Console.Clear();
      Console.WriteLine("\n===== REMOVE A BOOK =====");
      Console.WriteLine("\n1. Remove by title");
      Console.WriteLine("2. Remove by ISBN");
      Console.Write("\nEnter your choice (1-2): ");

      string choice = Console.ReadLine() ?? string.Empty;

      switch (choice)
      {
        case "1":
          RemoveBookByTitle();
          break;
        case "2":
          RemoveBookByISBN();
          break;
        default:
          Console.WriteLine("Invalid choice. Please try again.");
          break;
      }
    }

    private static void RemoveBookByTitle()
    {
      Console.Write("Enter the title of the book to remove: ");
      string title = Console.ReadLine() ?? "";

      try
      {
        if (library.RemoveBookByTitle(title))
        {
          Console.WriteLine($"Book '{title}' removed successfully.");
        }
        else
        {
          Console.WriteLine($"Book '{title}' not found.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error removing book: {ex.Message}");
      }
    }

    private static void RemoveBookByISBN()
    {
      Console.Write("Enter the ISBN of the book to remove: ");
      string isbn = Console.ReadLine() ?? "";

      try
      {
        if (library.RemoveBookByISBN(isbn))
        {
          Console.WriteLine($"Book with ISBN '{isbn}' removed successfully.");
        }
        else
        {
          Console.WriteLine($"Book with ISBN '{isbn}' not found.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error removing book: {ex.Message}");
      }
    }

    private static void SaveLibrary()
    {
      if (library.SaveToJson())
      {
        Console.WriteLine("Library data saved successfully.");
      }
      else
      {
        Console.WriteLine("Failed to save library data.");
      }
    }
  }
}
 */

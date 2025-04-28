// File: LibraryApp.cs

using System;
using System.Collections.Generic;

namespace SmartBook
{
    public class LibraryApp
    {
        private Library library;

        public LibraryApp()
        {
            library = new Library();
        }

        public void LoadLibrary()
        {
            try
            {
                library.LoadFromJson();
                Console.WriteLine("Library data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading library data: {ex.Message}");
                Console.WriteLine("Starting with an empty library.");
            }
        }

        public void Exit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using SmartBook!");

            if (library.SaveToJson())
                Console.WriteLine("Library data saved successfully before exit.");
            else
                Console.WriteLine("Failed to save library data before exit.");
        }

        public void ListAllBooks()
        {
            Console.Clear();
            Console.WriteLine("\n===== ALL BOOKS (sorted by title) =====");

            var books = library.GetAllBooksSortedByTitle();

            if (books.Count == 0)
                Console.WriteLine("No books found in the library.");
            else
            {
                DisplayBookList(books);
                Console.WriteLine($"\nTotal books: {books.Count}");
            }
        }

        public void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("\n===== SEARCH BOOKS =====");
            Console.Write("Enter search term (title, author, ISBN, or category): ");
            string searchTerm = Console.ReadLine() ?? string.Empty;

            var results = library.SearchBooks(searchTerm);

            if (results.Count == 0)
                Console.WriteLine("No books found matching the search term.");
            else
            {
                DisplayBookList(results);
                Console.WriteLine("-----------------------");
            }
            Console.WriteLine($"\nTotal books found: {results.Count}");
        }

        public void AddBook()
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

            var newBook = new Book(title, author, isbn, category);

            try
            {
                if (library.AddBook(newBook))
                    Console.WriteLine("Book added successfully!");
                else
                    Console.WriteLine("Failed to add book. Ensure all fields are filled and ISBN is unique.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
            }
        }

        public void RemoveBook()
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

        private void RemoveBookByTitle()
        {
            Console.Write("Enter the title of the book to remove: ");
            string title = Console.ReadLine() ?? "";

            try
            {
                if (library.RemoveBookByTitle(title))
                    Console.WriteLine($"Book '{title}' removed successfully.");
                else
                    Console.WriteLine($"Book '{title}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing book: {ex.Message}");
            }
        }

        private void RemoveBookByISBN()
        {
            Console.Write("Enter the ISBN of the book to remove: ");
            string isbn = Console.ReadLine() ?? "";

            try
            {
                if (library.RemoveBookByISBN(isbn))
                    Console.WriteLine($"Book with ISBN '{isbn}' removed successfully.");
                else
                    Console.WriteLine($"Book with ISBN '{isbn}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing book: {ex.Message}");
            }
        }

        public void SaveLibrary()
        {
            if (library.SaveToJson())
                Console.WriteLine("Library data saved successfully.");
            else
                Console.WriteLine("Failed to save library data.");
        }

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
    }
}

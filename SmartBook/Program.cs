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


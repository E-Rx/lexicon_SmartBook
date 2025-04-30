using SmartBook;

namespace LibraryTests;

public class UnitTest1
{
  [Fact]

  public void AddBook_ShouldAddBook_WhenBookIsValid()
  {
    var library = new Library();
    var book = new Book("Test Title", "Test Author", "1234567890", "Fiction");
    var result = library.AddBook(book);
    Assert.True(result);
    Assert.Contains(book, library.Books);

  }

  [Fact]
  public void AddBook_ShouldNotAddBook_WhenDuplicateISBN()
  {
    var library = new Library();
    var book1 = new Book("Test book 1", "Test Author 1", "1234567890", "Fantasy");
    var book2 = new Book("Test book 2", "Test Author 2", "1234567890", "Horror");

    library.AddBook(book1);
    var result = library.AddBook(book2);

    Assert.False(result);
    Assert.Single(library.Books);
  }

  [Fact]
  public void RemoveBookByTitle_ShouldRemoveBook_WhenBookExists()
  {
    var library = new Library();
    var book = new Book("Bonjour", "Elise", "4444444444", "Fiction");
    library.AddBook(book);
    var result = library.RemoveBookByTitle("Bonjour");
    Assert.True(result);
    Assert.DoesNotContain(book, library.Books);
  }

  [Fact]
  public void SearchBooks_ShouldReturnMatchingBooks_WhenSearchTermMatches()
  {
    var library = new Library();
    var book1 = new Book("Test Soif", "Test Amelie Nothomb", "9782226443885", "Test Fiction");
    var book2 = new Book("Test Lolita", "Test Vladimir Nabokov", "9782070412082", "Test Romantic drama ");
    library.AddBook(book1);
    library.AddBook(book2);

    var result = library.SearchBooks("Soif");

    Assert.Single(result);
    Assert.Contains(book1, result);
  }

  [Fact]
  public void SaveToJson_ShoudlSaveBookToJsonFile()
  {
    var library = new Library("test_library.json");
    var book = new Book("Test SaveToJson", "Test Json", "1234567890", "Fiction");
    library.AddBook(book);
    library.SaveToJson();

    Assert.True(File.Exists("test_library.json"));
  }

  [Fact]
  public void LoadFromJson_ShouldLoadBooksFromJsonFile()
  {
    var library = new Library("test_library.json");
    var book = new Book("Test LoadFromJson", "Test Json", "1111111111", "Test category");
    library.AddBook(book);
    library.SaveToJson();

    var newLibrary = new Library("test_library.json");
    newLibrary.LoadFromJson();

    Assert.Single(newLibrary.Books);
    Assert.Contains(newLibrary.Books, b =>
      b.Title == "Test LoadFromJson" &&
      b.Author == "Test Json" &&
      b.ISBN == "1111111111" &&
      b.Category == "Test category" &&
      b.IsAvailable == true);
  }


}

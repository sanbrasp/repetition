using OperationalBackendProgrammingRepetition.Models;

namespace OperationalBackendProgrammingRepetition.Catalogs;

public class BookCatalog
{
    private List<Book> _books;

    public BookCatalog()
    {
        _books = new List<Book>();
    }
    
    public void AddBook(string title, string author, int year, int isbn)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        author = author.Trim();
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentNullException(nameof(author));
        
        _books.Add(new Book(title, author, year, isbn));
    }

    public void RemoveBook(string title, int isbn)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        isbn = isbn;
        
        _books.RemoveAll(b => b.Title == title && b.ISBN == isbn);
    }
    
    public IEnumerable<Book> FindByISBN(int isbn)
    {
        return _books.Where(b => b.ISBN == isbn);
    }
    
    public IEnumerable<Book> FindByAuthor(string author)
    {
        return _books.Where(b => 
            b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }
    
    public IEnumerable<Book> GetAllBooks()
    {
        return _books.OrderBy(b => b.Title);
    }
}
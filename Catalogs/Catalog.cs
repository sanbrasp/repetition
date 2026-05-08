using OperationalBackendProgrammingRepetition.Interfaces;
using OperationalBackendProgrammingRepetition.Models;

namespace OperationalBackendProgrammingRepetition.Catalogs;

public class Catalog
{
    private List<Book> _books;
    private List<LibraryItem> _libraryItems;

    public Catalog()
    {
        _books = new List<Book>();
        _libraryItems = new List<LibraryItem>();
    }
    
    public void AddBook(string title, string author, int year, int isbn)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        author = author.Trim();
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentNullException(nameof(author));
        
        _books.Add(new Book(title, author, year, isbn));
    }
    
    public void AddMagazine(string title, int issue, string publisher, int year)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        publisher = publisher.Trim();
        if (string.IsNullOrWhiteSpace(publisher)) throw new ArgumentNullException(nameof(publisher));
        
        _libraryItems.Add(new Magazine(title, issue, publisher, year));
    }
    
    public void AddItem(LibraryItem item) => _libraryItems.Add(item);
    
    public void RemoveBook(string title, int isbn)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        
        _books.RemoveAll(b => b.Title == title && b.ISBN == isbn);
    }
    
    public IEnumerable<Book> GetAllBooks()
    {
        return _books.OrderBy(b => b.Title);
    }

    public IEnumerable<LibraryItem> GetAllLibraryItems()
    {
        return _libraryItems;
    }

    public List<LibraryItem> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<LibraryItem>();
        
        return _libraryItems
            .OfType<ISearchable>()
            .Where(item => item.MatchesQuery(query))
            .Cast<LibraryItem>()
            .ToList();
    }
}
using System.Globalization;
using Arbeidskrav1_Sem2.Helpers;
using OperationalBackendProgrammingRepetition.Catalogs;

namespace OperationalBackendProgrammingRepetition.UI;

public class ConsoleMenu
{
    Catalog _catalog = new Catalog();
    
    public void RunMenu()
    {
        
        
        bool showMenu = true;
        while (showMenu)
        {
            Console.Clear();
            Console.WriteLine("== Library Menu ==");
            Console.WriteLine();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. List all books");
            Console.WriteLine("3. Search by author");
            Console.WriteLine("0. Quit");

            var choice = InputHelpers.ReadMenuChoice("Choice: ", 0, 3);

            switch (choice)
            {
                case 1:
                    AddBook(); break;
                case 2:
                    ListAllBooks(); break;
                case 3:
                    SearchByAuthor(); break;
                case 0:
                    showMenu = false; break;
            }
        }
    }

    private void AddBook()
    {
        Console.Clear();
        Console.WriteLine("== Add Book ==");
        var title = InputHelpers.ReadRequiredString("Enter book title: ");
        var author = InputHelpers.ReadRequiredString("Enter book author: ");
        var year = InputHelpers.ReadInt("Enter book year: ");
        var isbn = InputHelpers.ReadInt("Enter book ISBN: ");
        Console.WriteLine();
        _catalog.AddBook(title, author, year, isbn);
        
        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    private void ListAllBooks()
    {
        Console.Clear();
        Console.WriteLine("== All Books ==");
        foreach (var book in _catalog.GetAllBooks())
        {
            Console.WriteLine(book);
            Console.WriteLine();
        }
        
        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    private void SearchByAuthor()
    {
        Console.Clear();
        Console.WriteLine("== Books by author ==");
        var author = InputHelpers.ReadRequiredString("Enter book author: ");
        foreach (var book in _catalog.FindBookByAuthor (author))
        {
            Console.WriteLine(author);
            Console.WriteLine();
        }
        
        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }
}
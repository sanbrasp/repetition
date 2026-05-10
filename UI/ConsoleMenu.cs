using System.Globalization;
using Arbeidskrav1_Sem2.Helpers;
using OperationalBackendProgrammingRepetition.Catalogs;
using OperationalBackendProgrammingRepetition.Models;
using OperationalBackendProgrammingRepetition.Services;

namespace OperationalBackendProgrammingRepetition.UI;

public class ConsoleMenu
{
    private readonly Catalog _catalog;
    private readonly LoanService _loanService;
    private readonly MemberService _memberService;

    public ConsoleMenu(Catalog catalog, LoanService loanService, MemberService memberService)
    {
        _catalog = catalog;
        _loanService = loanService;
        _memberService = memberService;
    }
    
    public void RunMenu()
    {
        bool running = true;
        while (running)
        {
            ShowMenu();
            

            var choice = InputHelpers.ReadMenuChoice("Choice: ", 0, 7);

            switch (choice)
            {
                case 1:
                    AddBook(); break;
                case 2:
                    ListAllItems(); break;
                case 3:
                    SearchItems(); break;
                case 4: 
                    RegisterMember(); break;
                case 5:
                    LoanItem();  break;
                case 6:
                    ReturnItem(); break;
                case 7:
                    GetMemberLoans(); break;
                case 0:
                    running = false; break;
                default:
                    Console.WriteLine("Invalid choice"); 
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    private void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("== Library Menu ==");
        Console.WriteLine();

        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Add book");
        Console.WriteLine("2. List all items");
        Console.WriteLine("3. Search.");
        Console.WriteLine("4. Register member");
        Console.WriteLine("5. Loan item");
        Console.WriteLine("6. Return item");
        Console.WriteLine("7. View member loan");
        Console.WriteLine("0. Quit");
    }

    private void AddBook()
    {
        Console.Clear();
        Console.WriteLine("== Add Book ==");
        var title = InputHelpers.ReadRequiredString("Enter book title: ");
        var author = InputHelpers.ReadRequiredString("Enter book author: ");
        var year = InputHelpers.ReadInt("Enter book year: ");
        var isbn = InputHelpers.ReadRequiredString("Enter book ISBN: ");
        Console.WriteLine();
        _catalog.AddBook(title, author, year, isbn);
        
        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    private void ListAllItems()
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

    private void RegisterMember()
    {
        Console.Clear();
        string name = InputHelpers.ReadRequiredString("Enter member name: ");
        string email = InputHelpers.ReadRequiredString("Enter member email: ");

        try
        {
            var member = _memberService.RegisterMember(name, email);
            Console.WriteLine($"Registered: {member.GetDisplayName()} (ID: {member.MemberId}.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private void LoanItem()
    {
        string isbn = InputHelpers.ReadRequiredString("Enter ISBN: ");
        string memberId =  InputHelpers.ReadRequiredString("Enter member id: ");

        try
        {
            var item = _catalog.FindByISBN(isbn)
                ?? throw new InvalidOperationException($"Item with ISBN {isbn} not found.");
            var member = _memberService.FindById(memberId)
                ?? throw new InvalidOperationException($"Item with ID {memberId} not found.");
            
            _loanService.LoanItem(item, member);
            Console.WriteLine($"Loaned '{item.Title} to {member.GetDisplayName()}'");
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private void ReturnItem() // Who would return a book based on the ISBN...?
    {
        string isbn = InputHelpers.ReadRequiredString("Enter ISBN: ");

        try
        {
            var item = _catalog.FindByISBN(isbn)
                ?? throw new InvalidOperationException($"Item with ISBN {isbn} not found.");
            
            _loanService.ReturnItem(item);
            Console.WriteLine($"'{item.Title}' returned.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private void SearchItems()
    {
        Console.Clear();
        string query =  InputHelpers.ReadRequiredString("Search: ");
        var results = _catalog.Search(query);
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }

    private void GetMemberLoans()
    {
        string memberId = InputHelpers.ReadRequiredString("Enter member id: ");

        try
        {
            var member = _memberService.FindById(memberId)
                         ?? throw new InvalidOperationException($"Member with ID {memberId} not found.");

            var loans = _loanService.GetLoansForMember(member);

            if (loans.Count == 0)
            {
                Console.WriteLine($"{member.GetDisplayName()} has no active loans.");
                return;  // semicolon, not colon
            }

            Console.WriteLine($"Active loans for {member.GetDisplayName()}:");
            foreach (var loan in loans)
            {
                string overdue = loan.IsOverdue ? " OVERDUE" : string.Empty;
                Console.WriteLine($"  - '{loan.Item.Title}' due {loan.DueDate:dd MMM yyyy}{overdue}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
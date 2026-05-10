using OperationalBackendProgrammingRepetition.Models;

namespace OperationalBackendProgrammingRepetition.Services;

public class LoanService
{
    private readonly List<Loan> _loans;
    
    public LoanService()
    {
        _loans = new List<Loan>();
    }

    public void LoanItem(LibraryItem item, Member member)
    {
        if (!item.IsAvailable)
            throw new InvalidOperationException($"Item {item} is not available");
        
        item.IsAvailable = false;
        
        var loan = new Loan(item, member);
        _loans.Add(loan);
    }

    public void ReturnItem(LibraryItem item)
    {
        var loan = _loans.FirstOrDefault(l => l.Item == item && !l.IsReturned);
        
        if (loan == null)
            throw new InvalidOperationException($"{item.Title} has no active loan record.");
        
        loan.MarkReturned();
        item.IsAvailable = true;
    }
    
    public List<Loan> GetLoansForMember(Member member)
    {
        return _loans
            .Where(l => l.Member == member && !l.IsReturned)
            .ToList();
    }

    public IEnumerable<Loan> GetOverdueLoans()
    {
        return _loans
            .Where(l => l.IsOverdue)
            .ToList();
    }
}
using Microsoft.VisualBasic;

namespace OperationalBackendProgrammingRepetition.Models;

public class Loan
{
    public LibraryItem Item { get; }
    public Book Book { get; }
    public Member Member { get; }
    
    public DateTime? ReturnedDate { get; private set; }
    public DateTime LoanDate { get; }
    public DateTime DueDate { get; }
    
    public bool IsOverdue => DateTime.Now > DueDate;
    public bool IsReturned => !IsReturned && DateTime.Today > DueDate;
    
    public Loan(LibraryItem item, Member member, int loanDays = 14)
    {
        Item = item;
        Member = member;
        LoanDate = DateTime.Today;
        DueDate = DateTime.Today.AddDays(loanDays);
    }

    public void MarkReturned()
    {
        ReturnedDate = DateTime.Today;
    }
}
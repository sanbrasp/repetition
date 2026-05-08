namespace OperationalBackendProgrammingRepetition.Models;

public class Loan
{
    public Book Book { get; }
    public Member Member { get; }
    
    private DateTime _loanDate;
    private DateTime _dueDate;
    
    public bool IsOverdue => DateTime.Now > _dueDate;
    
    public Loan(Book book, Member member, DateTime dueDate)
    {
        this.Book = book;
        this.Member = member;
        _loanDate = DateTime.Now;
        _dueDate = dueDate;
    }
}
using OperationalBackendProgrammingRepetition.Interfaces;

namespace OperationalBackendProgrammingRepetition.Models;

public class Magazine : LibraryItem, ISearchable
{
    private int _issueNumber;
    private string _publisher;
    private string _title;
    
    public int IssueNumber => _issueNumber;
    public string Publisher => _publisher;
    public string Title => _title;
    
    public Magazine(string title, int issueNumber, string publisher, int year) : 
        base(title, year)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be empty.");
        if (string.IsNullOrWhiteSpace(publisher)) throw new ArgumentNullException(nameof(publisher), "Publisher name cannot be empty.");
        
        _issueNumber = issueNumber;
        _publisher = publisher;
    }

    public override string GetItemType() => "Magazine";

    public bool MatchesQuery(string query)
    {
        string q = query.Trim().ToLower();
        return Title.ToLower().Contains(q) || Publisher.ToLower().Contains(q);
    }
}
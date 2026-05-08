namespace OperationalBackendProgrammingRepetition.Models;

public abstract class LibraryItem
{
    protected Guid _id { get; init; }

    public bool IsAvailable;

    internal string Title { get; init; }
    
    protected int Year { get; init; }


    protected LibraryItem(string title, int year)
    {
        _id = new Guid();
        Title = title;
        Year = year;
        IsAvailable = true;
    }
    
    
    public bool SetAvailable()
    {
        if (IsAvailable == false)
            IsAvailable = true;
        return IsAvailable;
    }

    public bool SetUnavailable()
    {
        if (IsAvailable)
            IsAvailable = false;
        return IsAvailable;
    }

    public abstract string GetItemType();

    public override string ToString()
    {
        return ($"Title: {Title}\n" +
                $"Year: {Year}\n" + 
                $"IsAvailable: {IsAvailable}");
    }
}
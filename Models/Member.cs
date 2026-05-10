namespace OperationalBackendProgrammingRepetition.Models;

public class Member
{
    private Guid _memberId;
    
    private string _name;
    private string _email;

    public Guid MemberId => _memberId;
    
    public string Name
    {
        get => _name;
        private set => _name = value;
    }
    public string Email
    {
        get => _email;
        private set => _email = value;
    }
    
    public Member(string name, string email)
        {
        _memberId = Guid.NewGuid();
        _name = name;
        _email = email;
        }

    public string GetDisplayName()
    {
        return $"ID: {_memberId} - {Name} - {Email}";
    }
}
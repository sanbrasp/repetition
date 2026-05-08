namespace OperationalBackendProgrammingRepetition.Models;

public class Member
{
    private Guid _id;
    
    private string _name;
    private string _email;

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
    public Guid Id => _id;
    
    public Member(string name, string email)
        {
        _id = Guid.NewGuid();
        _name = name;
        _email = email;
        }

    public string GetDisplayName()
    {
        return $"ID: {_id} - {Name} - {Email}";
    }
}
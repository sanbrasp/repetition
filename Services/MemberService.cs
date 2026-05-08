using OperationalBackendProgrammingRepetition.Models;

namespace OperationalBackendProgrammingRepetition.Services;

public class MemberService
{
    private readonly List<Member> _members;

    public MemberService()
    {
        _members = new List<Member>();
    }

    public void RegisterMember(string name, string email)
    {
        name = name.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        email = email.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
        if (_members.Exists(m => m.Name == name && m.Email == email)) throw new ArgumentException($"Member with name {name} already exists");
        
        var member = new Member(name, email);
        _members.Add(member);
    }

    public void FindById()
    {
        
    }

    public void FindByName()
    {
        
    }

    public void GetAllMembers()
    {
        
    }
}
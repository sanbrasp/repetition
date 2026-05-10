using OperationalBackendProgrammingRepetition.Models;

namespace OperationalBackendProgrammingRepetition.Services;

public class MemberService
{
    private readonly List<Member> _members;

    public MemberService()
    {
        _members = new List<Member>();
    }

    public Member RegisterMember(string name, string email)
    {
        name = name.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        email = email.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
        if (_members.Exists(m => m.Name == name && m.Email == email)) throw new ArgumentException($"Member with name {name} already exists");
        
        var member = new Member(name, email);
        _members.Add(member);
        
        return member;
    }

    public Member? FindById(string id)
    {
        if (!Guid.TryParse(id, out Guid guid))
            return null;
        
        return _members
            .FirstOrDefault(m => m.MemberId == guid);
    }

    public IEnumerable<Member> FindByName(string name)
    {
        return _members
            .Where(m => m.Name == name)
            .ToList();
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return _members.ToList();
    }
}
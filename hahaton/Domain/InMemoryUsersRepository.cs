namespace Domain;

public class InMemoryUsersRepository : IUsersRepository
{
    private Dictionary<Guid, User> _users = new();
    
    public User? GetByEmail(string email)
    {
        return _users.Values.FirstOrDefault(u => u.Email == email);
    }

    public User? GetById(Guid id)
    {
        return _users.Values.FirstOrDefault(u => u.Id == id);
    }

    public void Add(User user)
    {
        _users.Add(user.Id, user);
    }
}
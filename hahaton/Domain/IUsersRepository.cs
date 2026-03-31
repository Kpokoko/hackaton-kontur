namespace Domain;

public interface IUsersRepository
{
    public User? GetByEmail(string email);
    public User? GetById(Guid id);
    
    public void Add(User user);
}
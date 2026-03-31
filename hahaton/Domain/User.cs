namespace Domain;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public List<AddCustomTypeRequest> CustomTypes { get; set; } = new();

    public User(Guid id, string email, string hashedPassword)
    {
        Id = id;
        Email = email;
        HashedPassword = hashedPassword;
    }
}

public enum Format
{
    Email,
    Phone,
    Name,
    None
}

public record AddCustomTypeRequest(string Name, Structure[] Structure);
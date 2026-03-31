using Domain;
using Infrastructure;
using MockServiceApplication.Identity.JWT;


namespace Application.Identity.Services;

public class AuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public Guid Register(string email, string password)
    {
        var existing = _usersRepository.GetByEmail(email);
        if (existing != null)
        {
            throw new Exception(email);
        }
        
        var hashedPassword = _passwordHasher.Generate(password);

        var userId = Guid.NewGuid();
        
        var user = new User(
            userId,
            email,
            hashedPassword);

        _usersRepository.Add(user);

        return userId;
    }
    
    public string Login(string email, string password)
    {
        var user = _usersRepository.GetByEmail(email);
    
        if (user is null)
        {
            throw new Exception();
        }

        if (!_passwordHasher.Verify(password, user.HashedPassword))
        {
            throw new Exception();
        }

        return _jwtProvider.GenerateToken(user);
    }
}

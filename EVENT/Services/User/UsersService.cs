using Microsoft.EntityFrameworkCore;

public class UsersService : IUsersService
{
    private readonly DataBase _context;
    private readonly TokenGenerator _tokenGenerator;
    private readonly PasswordHasher _passwordHasher;
    public UsersService(DataBase context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher();
    }

    public async Task<string?> Registration(UserRegistrationModel body)
    {
        var checkEmail = await _context.Users.AllAsync(u => u.Email != body.Email);
        if (!checkEmail)
        {
            throw new Exception($"User with email:{body.Email} already exists");
        }

        var token = TokenGenerator.GenerateToken();
        var hashedPassword = _passwordHasher.HashPassword(body.Password);
        var user = new User
        {
            Name = body.Name,
            Password = hashedPassword,
            Email = body.Email,
            Token = token
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return token;
    }

    public async Task<string?> Authorization(UserAuthorizationModel body)
    {
        var token = TokenGenerator.GenerateToken();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == body.Email);
      


        if (user == null)
        {
            throw new NotCalendarException("User not Found");
        }
        if (user.Password != _passwordHasher.HashPassword(body.Password))
        {
            throw new Exception("Incorrect Password");
        }

        user.Token = token;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return token;
    }
}

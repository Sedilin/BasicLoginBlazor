using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace WebApplication1.Services;

public class AuthService : IAuthService
{

    private readonly IList<User> users = new List<User>
    {
        new User
        {
            Age = 23,
            Email = "luskk@via.dk",
            Domain = "via",
            Name = "Lukasz Sramkowski",
            Password = "0605",
            Role = "Student",
            Username = "luskk",
            SecurityLevel = 2
        },
        new User
        {
            Age = 34,
            Email = "trols@gmail.com",
            Domain = "gmail",
            Name = "Trols Rasmussen",
            Password = "1234",
            Role = "Teacher",
            Username = "trols",
            SecurityLevel = 4
        }
    };
        
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new Exception("Fields cannot be empty");
        }
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}
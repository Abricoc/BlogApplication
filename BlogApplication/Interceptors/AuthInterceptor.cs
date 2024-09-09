using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models.Auth;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BlogApplication.Interceptors;

public class AuthInterceptor(BlogContext context)
{
    public async Task Register(RegisterRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            Password = BC.HashPassword(request.Password),
            FullName = request.FullName
        };
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
    
    public async Task<bool> CheckEmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }
    
    public async Task<User?> CheckPassword(string email, string password)
    {
        var user = await context
            .Users
            .Where(u => u.Email == email).FirstOrDefaultAsync();

        if (user is not null && BC.Verify(password, user.Password))
        {
            return user;
        }

        return null;
    }
}
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models.Auth;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BlogApplication.Interceptors;

public class TagInterceptor(BlogContext context)
{
    public async Task<List<Tag>> GetAllTags()
    {
        return await context.Tags.ToListAsync();
    }
}
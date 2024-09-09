using System.Diagnostics;
using System.Security.Claims;
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Interceptors;
using Microsoft.AspNetCore.Mvc;
using BlogApplication.Models;
using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers;

public class PostController(
    TagInterceptor tagInterceptor,
    PostInterceptor postInterceptor) : Controller
{
    public async Task<IActionResult> Index([FromQuery(Name = "tag")] string? tag)
    {
        return View(new PostListModel
        {
            Posts = await postInterceptor.GetAllPosts(tag ?? ""),
            Tags = await tagInterceptor.GetAllTags()
        });
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(PostCreateRequest request)
    {
        if (!ModelState.IsValid) return View(request);
        var post = await postInterceptor
            .CreatePost(
                request, 
                Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")
            );
        
        return RedirectToAction("View", "Post", new {id = post.Id});
    }
    
    public async Task<IActionResult> View(Guid id)
    {
        var post = await postInterceptor.GetPostById(id);
        if (post is null)
        {
            return NotFound();
        }
        
        return View(post);
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        await postInterceptor.DeletePost(id);
        
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
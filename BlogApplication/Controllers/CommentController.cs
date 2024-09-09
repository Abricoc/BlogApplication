using System.Security.Claims;
using BlogApplication.Interceptors;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

public class CommentController(CommentInterceptor commentInterceptor) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create(Guid postId, string commentText)
    {
        await commentInterceptor.CreateComment(postId, commentText, Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid) ?? ""));
        
        return RedirectToAction("View", "Post", new {id = postId});
    }
}
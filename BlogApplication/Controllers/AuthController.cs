using System.Security.Claims;
using BlogApplication.Interceptors;
using BlogApplication.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

[Controller]
public class AuthController(AuthInterceptor authInterceptor) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid) return View(request);
        var user = await authInterceptor.CheckPassword(request.Email, request.Password);
        if (user is null)
        {
            ModelState.AddModelError("Email", "Неверный пароль или email");
            return View(request);
        }
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.PrimarySid, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Name, user.FullName)
        };
        
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        
        return RedirectToAction("Index", "Post");
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await authInterceptor.CheckEmailExists(request.Email))
        {
            ModelState.AddModelError("Email", "Email уже зарегистрирован");
        }
        if (!ModelState.IsValid) return View(request);
        
        await authInterceptor.Register(request);
        return RedirectToAction("Login");

    }
}
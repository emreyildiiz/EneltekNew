using Demo.Data;
using Demo.Models;
using Demo.Modelsw;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {

        private readonly EneltekDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(EneltekDbContext db,ILogger<HomeController>logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (System.Security.Claims.ClaimsPrincipal.Current != null)
            {
                if (System.Security.Claims.ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value != null)
                {
                    RedirectToAction("Index", "Icerik");
                }
            }

            return View();
        }
        [HttpPost]

        [AllowAnonymous]
        public async Task<IActionResult> Index([Bind] Kullanici k)
        {
            Kullanici x;
            bool basarili = false;
            try
            {
              
                using (_db)
                {
                    x = _db.Kullanicilar.FirstOrDefault(x => x.UserName == k.UserName && x.Password == k.Password);
                    if (x != null)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,k.UserName),
                        new Claim(ClaimTypes.Role,k.YetkiId.ToString())
                    };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var useridentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                        principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal, GetAuthenticationProperites(k.BeniHatirla));
                     
                        return RedirectToAction("Index", "Icerik");
                    }
                    else
                    {
                        await HttpContext.SignOutAsync();
                    }
                }
                TempData["Login"] = basarili;
                return View(x);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return View();
            }
        }
        
        public AuthenticationProperties GetAuthenticationProperites(bool isPersistent = false)
        {
            if (isPersistent == false)
            {
                return new AuthenticationProperties()
                {
                    IsPersistent = isPersistent,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                    RedirectUri = "/Index"
                };
            }
            else
            {
                return new AuthenticationProperties()
                {
                    IsPersistent = isPersistent,
                    //ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    RedirectUri = "/Index"
                };
            }

        }
        public IActionResult Index2()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Demo.Data;
using Demo.Modelsw;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class IcerikController : Controller
    {
        private readonly EneltekDbContext dbContext;
        private readonly ILogger<IcerikController> _logger;
        public IcerikController(EneltekDbContext db, ILogger<IcerikController> logger)
        {
            dbContext = db;
            this._logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> KullaniciIslemleri()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getUsers()
        {
            try
            {
                var kullanici = dbContext.Kullanicilar.ToList();
                return Json(kullanici);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
        [HttpPost]
        public IActionResult addUser([Bind] Kullanici k)
        {
            try
            {
                dbContext.Kullanicilar.Add(k);
                dbContext.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("KullaniciIslemleri", "Icerik");
        }

    }
}

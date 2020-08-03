using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Models;

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET - INDEX
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _db.New.Where(x => x.CategoryId == id).Include(x => x.Category).ToListAsync();
            Categories categories = await _db.Category.FindAsync(id);
            ViewBag.CategoryName = categories.Name;

            return View(news);
        }

        //GET - UPDATE
        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var news = await _db.New.FindAsync(id);
            ViewBag.CatId = news.CategoryId;

            return View(news);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePOST(New news)
        {
            if (news == null)
            {
                return NotFound();
            }

            news.Date = DateTime.Now;

            _db.New.Update(news);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "News", new {id = news.CategoryId });
        }

        //GET - DELETE
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _db.New.FindAsync(id);
            ViewBag.CatId = news.CategoryId;

            return View(news);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _db.New.FindAsync(id);
            _db.New.Remove(news);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "News", new { id = news.CategoryId });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Data;

namespace News.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET - INDEX
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Category.ToListAsync();
            return View(categories);
        }
    }
}

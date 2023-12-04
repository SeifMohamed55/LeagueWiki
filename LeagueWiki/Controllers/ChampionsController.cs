using LeagueWiki.Data;
using LeagueWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LeagueWiki.Controllers
{
    public class ChampionsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;      

        public ChampionsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Champions.Include(c => c.Country).Include(r => r.Role).ToList());

        }

        public IActionResult Search(string searchTerm)
        {
            if(!string.IsNullOrWhiteSpace(searchTerm))   
                return PartialView("_ShownData", _context.Champions.Where(c => (c.Name.Contains(searchTerm) || c.Role.Name.Contains(searchTerm) || c.Country.Name.Contains(searchTerm))).Include(c => c.Country).Include(r => r.Role).ToList());
            else
                return PartialView("_ShownData", _context.Champions.Include(c => c.Country).Include(r => r.Role).ToList());
        }

        public IActionResult GetAddView()
        {
            ViewBag.AllRoles = _context.Roles.ToList();
            ViewBag.AllCountries = _context.Countries.ToList();
            return View("Create");
        }

        public IActionResult AddChampion(Champion champion, IFormFile? imgFormFile)
        {
            if (champion.AddDate < new DateTime(2009, 1, 1))
                ModelState.AddModelError(String.Empty, "Cannot Add a champion before 1/1/2009");

            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    string extension = Path.GetExtension(imgFormFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string newFileName = guid + extension;
                    string relativePath = "\\images\\champions\\" + newFileName;
                    champion.ImagePath = relativePath;
                    string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }
                else
                {
                    champion.ImagePath = "\\images\\No_Image.png";
                }

                _context.Champions.Add(champion);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                ViewBag.AllRoles = _context.Roles.ToList();
                ViewBag.AllCountries = _context.Countries.ToList();
                return View("Create", champion);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Champion champion = _context.Champions.Include(ch => ch.Role).Include(ch => ch.Country).FirstOrDefault(ch => ch.Id == id);
            if (champion == null)
                return NotFound();
            return View("Details", champion);
        }

        public IActionResult GetEditView(int id)
        {
            ViewBag.AllRoles = _context.Roles.ToList();
            ViewBag.AllCountries = _context.Countries.ToList();
            return View("Edit", _context.Champions.FirstOrDefault(c => c.Id == id));
        }

        public IActionResult EditChampion(Champion champion, IFormFile? imgFormFile)
        {
            string oldPath = champion.ImagePath;
            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    if (champion.ImagePath != "\\images\\No_Image.png")
                    {
                        System.IO.File.Delete(_webHostEnvironment.WebRootPath + oldPath);
                    }

                    string extension = Path.GetExtension(imgFormFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string newFileName = guid + extension;
                    string relativePath = "\\images\\countries\\" + newFileName;
                    champion.ImagePath = relativePath;
                    string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Dispose();

                }
                _context.Champions.Update(champion);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                ViewBag.AllRoles = _context.Roles.ToList();
                ViewBag.AllCountries = _context.Countries.ToList();
                return View("Edit", champion);
            }
        }

        public IActionResult GetDeleteView(int id)
        {
            Champion champion = _context.Champions.Include(ch => ch.Role).Include(ch => ch.Country).FirstOrDefault(ch => ch.Id == id);
            if(champion == null) return NotFound();

            return View("Delete", champion);
        }
        public IActionResult DeleteChampion(int id)
        {
            if (id < 1)
                return BadRequest();
            Champion champion = _context.Champions.FirstOrDefault(d => d.Id == id);
            if (champion != null)
            {
                if (champion.ImagePath != "\\images\\No_Image.png")
                {
                    System.IO.File.Delete(_webHostEnvironment.WebRootPath + champion.ImagePath);
                }
                _context.Champions.Remove(champion);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                return BadRequest();
            }

        }


    }
}

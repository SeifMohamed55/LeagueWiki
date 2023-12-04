using LeagueWiki.Data;
using LeagueWiki.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LeagueWiki.Controllers
{
    public class CountriesController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public CountriesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public IActionResult GetIndexView(/*string? search*/)
        {
            //if (string.IsNullOrWhiteSpace(search))
            //{
            //    return View("Index", _context.Countries.ToList());
            //}
            //ViewBag.searchVal = search;
            //return View("Index", _context.Countries.Where(c => c.Name.Contains(search)) );
            return View("Index", _context.Countries.ToList());
        }

        public IActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return PartialView("_ShownData", _context.Countries.Where(c => c.Name.Contains(searchTerm)));
            }
            return PartialView("_ShownData",_context.Countries.ToList());
        }

        public IActionResult GetAddView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddCountry(Country country, IFormFile? imgFormFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    string extension = Path.GetExtension(imgFormFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string newFileName = guid + extension;
                    string relativePath = "\\images\\countries\\" + newFileName;
                    country.ImagePath = relativePath;
                    string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }
                else
                {
                    country.ImagePath = "\\images\\No_Image.png";
                }

                _context.Countries.Add(country);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                return View("Create", country);
            }
        }

        public IActionResult GetEditView(int id)
        {
            if (id < 1)
                return NotFound();
            return View("Edit", _context.Countries.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        public IActionResult EditCountry(Country country, IFormFile? imgFormFile)
        {
            string oldPath = country.ImagePath;
            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    if (country.ImagePath != "\\images\\No_Image.png")
                    {
                        System.IO.File.Delete(_webHostEnvironment.WebRootPath + oldPath);
                    }

                        string extension = Path.GetExtension(imgFormFile.FileName);
                        Guid guid = Guid.NewGuid();
                        string newFileName = guid + extension;
                        string relativePath = "\\images\\countries\\" + newFileName;
                        country.ImagePath = relativePath;
                        string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                        FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                        imgFormFile.CopyTo(fileStream);
                        fileStream.Dispose();
                    
                }
                _context.Countries.Update(country);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                return View("Edit", country);
            }
        }


        public IActionResult GetDetailsView(int id)
        {
            if (id < 1)
                return NotFound();
            ViewBag.Champions = _context.Champions.Where(ch=> ch.CountryId == id).ToList();
            return View("Details", _context.Countries.FirstOrDefault(c => c.Id == id));
        }

        public IActionResult GetDeleteView(int id)
        {
            if (id < 1)
                return NotFound();
            ViewBag.Champions = _context.Champions.Where(ch => ch.CountryId == id).ToList();
            return View("Delete", _context.Countries.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        public IActionResult DeleteCountry(int id)
        {
            if (id < 1)
                return BadRequest();
            Country country = _context.Countries.FirstOrDefault(d => d.Id == id);
            if(country != null)
            {
                if(country.ImagePath != "\\images\\No_Image.png")
                {
                System.IO.File.Delete(_webHostEnvironment.WebRootPath + country.ImagePath);
                }
                _context.Countries.Remove(country);
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

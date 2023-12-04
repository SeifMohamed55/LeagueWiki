using LeagueWiki.Data;
using LeagueWiki.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;

namespace LeagueWiki.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public RolesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult GetIndexView()
        {    
            return View("Index", _context.Roles.ToList());
        }

        public IActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return PartialView("_ShownData", _context.Roles.Where(x => x.Name.Contains(searchTerm)).ToList());
            }
            else
            {
                return PartialView("_ShownData", _context.Roles.ToList());
            }
        }

        public IActionResult GetAddView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddRole(Role role, IFormFile? imgFormFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    string extension = Path.GetExtension(imgFormFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string newFileName = guid + extension;
                    string relativePath = "\\images\\roles\\" + newFileName;
                    role.ImagePath = relativePath;
                    string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }
                else
                {
                    role.ImagePath = "\\images\\No_Image.png";
                }

                _context.Roles.Add(role);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                return View("Create", role);
            }
        }


        public IActionResult GetEditView(int id)
        {
            if (id < 1)
                return BadRequest();
            Role role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
                return NotFound();

            return View("Edit", role);
        }

        [HttpPost]
        public IActionResult EditRole(Role role, IFormFile? imgFormFile)
        {

            string oldPath = role.ImagePath;
            if (ModelState.IsValid)
            {
                if (imgFormFile != null)
                {
                    if (role.ImagePath != "\\images\\No_Image.png")
                    {
                        System.IO.File.Delete(_webHostEnvironment.WebRootPath + oldPath);
                    }

                    string extension = Path.GetExtension(imgFormFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string newFileName = guid + extension;
                    string relativePath = "\\images\\roles\\" + newFileName;
                    role.ImagePath = relativePath;
                    string fullPath = _webHostEnvironment.WebRootPath + relativePath;
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    imgFormFile.CopyTo(fileStream);
                    fileStream.Dispose();


                }
                _context.Roles.Update(role);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");

            }
            else
            {
                return View("Edit", role);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            if (id < 1)
                return BadRequest();
            Role role = _context.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return NotFound();
            ViewBag.Champions = _context.Champions.Where(ch => ch.RoleId == id).ToList();
            return View("Details", role);
        }
        public IActionResult GetDeleteView(int id)
        {
            if (id < 1)
                return BadRequest();
            Role role = _context.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return NotFound();
            ViewBag.Champions = _context.Champions.Where(ch => ch.RoleId == id).ToList();
            return View("Delete", role);
        }


        [HttpPost]
        public IActionResult DeleteRole(int id)
        {
            Role role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                if (role.ImagePath != "\\images\\No_Image.png")
                {
                    System.IO.File.Delete(_webHostEnvironment.WebRootPath + role.ImagePath);
                }
                _context.Roles.Remove(role);
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

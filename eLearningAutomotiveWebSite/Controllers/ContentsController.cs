using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLearningAutomotiveWebSite.Data;
using eLearningAutomotiveWebSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace eLearningAutomotiveWebSite.Controllers
{
    public class ContentsController : Controller
    {
        private readonly eLearningAutomotiveWebSiteContext _contextDb;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private string _userId = "";
        private string _userRole = "visitor";
        private string _userEmail = "dude";
        private string[] Categories = { "NA","carrosserie", "moteur", "mécanique", "entretien" };

    public ContentsController(eLearningAutomotiveWebSiteContext contextDb,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _contextDb = contextDb;
            this.signInManager = signInManager; // à supprimer ?
            this.userManager = userManager;
            if (User is not null)
            {
                _userId = userManager.GetUserId(User);
                _userEmail = userManager.GetUserName(User);
            }
        }

        public IActionResult Index()
        {
            IEnumerable<Content> Contents = _contextDb.Content;
            IEnumerable<History> History = _contextDb.History;
            _userRole = User.IsInRole("customer") ? "customer" : User.IsInRole("employee") ? "employee" : User.IsInRole("superadmin") ? "superadmin" : "visitor";
            ViewBag.Role = _userRole;
            ViewBag.Email = _userEmail;
            ViewBag.Categories = Categories;
            var idsHistoryUser = History.Where(x => x.IdUser == _userId).ToList();
            foreach (Content content in Contents) // avec limitation du nombre si qté importante de contenu
            {
                if (idsHistoryUser.Count() == 0) content.DejaVu = false;
                else if (idsHistoryUser.Where(x => x.IdContent == content.Id).Count() > 0) content.DejaVu = true;
                else content.DejaVu = false;
            }
            return View(Contents);
        }
        [HttpGet]
        [Authorize(Roles = "employee")]
        public IActionResult Create()
        {
            if (User.IsInRole("employee")) return View(); // option: || User.IsInRole("superadmin")
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "employee")]
        public IActionResult Edit(int id) // sélection pour modification d'objet
        {
            if (User.IsInRole("visitor") || User.IsInRole("customer")) return RedirectToAction("Index");
            if (id == 0)
            {
                return NotFound();
            }
            var Contents = _contextDb.Content.FirstOrDefault(c => c.Id == id);
            // ou _db.Content.Find(id)
            if (Contents is null)
            {
                return NotFound();
            }
            return View(Contents);
        }
        [HttpGet]
        [Authorize(Roles = "employee")]
        public IActionResult Delete(int id)
        {
            if (User.IsInRole("visitor") || User.IsInRole("customer")) return RedirectToAction("Index");
            if (id == 0)
            {
                return NotFound();
            }
            var Contents = _contextDb.Content.FirstOrDefault(c => c.Id == id);
            // ou _db.Content.Find(id)
            if (Contents is null)
            {
                return NotFound();
            }
            return View(Contents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee")]
        public IActionResult Create(Content Contents)
        {
            if (User.IsInRole("visitor") || User.IsInRole("customer")) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                _contextDb.Content.Add(Contents);
                _contextDb.SaveChanges();
                TempData["message"] = "Tuto ajouté";
            }
            else ModelState.AddModelError("Tuto", "donnée incorrecte");

            return View();
            // ou redirect vers Index: RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee")]
        public IActionResult Edit(Content Contents)
        {
            if (User.IsInRole("visitor") || User.IsInRole("customer")) return RedirectToAction("Index");
            if (Contents is not null)
            {
                if (ModelState.IsValid)
                {
                    _contextDb.Content.Update(Contents);
                    _contextDb.SaveChanges();
                    TempData["message"] = "Tuto édité";
                    return RedirectToAction("Index");
                }
                else return View(Contents);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee")]
        public IActionResult Delete(Content Contents) // suppression d'objet en BDD
        {
            if (User.IsInRole("visitor") || User.IsInRole("customer")) return RedirectToAction("Index");
            if (Contents is not null)
            {
                _contextDb.Content.Remove(Contents);
                _contextDb.SaveChanges();
                TempData["message"] = "Tuto supprimé";
                return RedirectToAction("Index");
            }
            else return View(Contents);
        }
    }
}

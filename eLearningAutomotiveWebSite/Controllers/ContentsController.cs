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

        public ContentsController(eLearningAutomotiveWebSiteContext contextDb,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _contextDb = contextDb;
            this.signInManager = signInManager;
        }
        public IActionResult Index() // visiteurs uniquement (donc texte)
        {
            ViewBag.role = ""; // tous sauf visitor
            IEnumerable<Content> Contents = _contextDb.Content;
            IEnumerable<History> History = _contextDb.History;
            if (User.IsInRole("visitor")) ViewBag.role = "v";
            else
            {
                var idHistory = Contents.Where(x => x.IdContent == _userManager.GetUserId(HttpContext.User)).Select(y => y.IdContent);
                var ContentList = _context.Content.Where(c => idcontents.Contains(c.Id));
                foreach (Content content in Contents)
                {
                    foreach(History history in History) //modif
                    {
                        if ((history.IdUser == User) && (Content.Id == History.IdContent)) Content.DejaVu = true;
                        else Content.DejaVu = false;
                    }
                }
            }
            return View(Contents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (User.IsInRole("employee") || User.IsInRole("superAdmin")) return View();
            return RedirectToAction("Index");
        }
        [HttpGet]
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

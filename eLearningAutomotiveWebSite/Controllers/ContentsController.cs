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

        public ContentsController(eLearningAutomotiveWebSiteContext contextDb)
        {
            _contextDb = contextDb;
        }
        [Authorize(Roles ="visitor")]
        public IActionResult Index() // visiteurs uniquement (donc texte)
        {
            IEnumerable<Content> Contents = _contextDb.Content;
            return View(Contents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id) // sélection pour modification d'objet
        {
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
        public IActionResult Add(Content Contents)
        {
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

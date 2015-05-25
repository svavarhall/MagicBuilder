using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MagicBuilder.Models;
using Microsoft.AspNet.Identity;

namespace MagicBuilder.Controllers
{
    public class DecksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Decks
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            List<Deck> decks = db.Decks.Where(t => t.UserId == userId).ToList();
           
            return View(decks);
        }

        // GET: Decks/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // GET: Decks/Create
        [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Decks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "DeckID,Name")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                deck.UserId = User.Identity.GetUserId();
                db.Decks.Add(deck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deck);
        }

        // GET: Decks/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "DeckID,Name")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        // GET: Decks/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            Deck deck = db.Decks.Find(id);
            db.Decks.Remove(deck);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

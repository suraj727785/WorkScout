using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkScout.Models;

namespace WorkScout.Controllers
{
    public class RecruitersController : Controller
    {
        private WorkScoutEntities db = new WorkScoutEntities();

        // GET: Recruiters
        public ActionResult Index()
        {
            var recruiters = db.Recruiters.Include(r => r.UserDetail);
            return View(recruiters.ToList());
        }

        // GET: Recruiters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // GET: Recruiters/Create
        public ActionResult Create()
        {
            ViewBag.RecruiterEmail = new SelectList(db.UserDetails, "Email", "Name");
            return View();
        }

        // POST: Recruiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecruiterEmail,CompanyName,CompanyWebsiteUrl,Designation")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Recruiters.Add(recruiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecruiterEmail = new SelectList(db.UserDetails, "Email", "Name", recruiter.RecruiterEmail);
            return View(recruiter);
        }

        // GET: Recruiters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecruiterEmail = new SelectList(db.UserDetails, "Email", "Name", recruiter.RecruiterEmail);
            return View(recruiter);
        }

        // POST: Recruiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecruiterEmail,CompanyName,CompanyWebsiteUrl,Designation")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecruiterEmail = new SelectList(db.UserDetails, "Email", "Name", recruiter.RecruiterEmail);
            return View(recruiter);
        }

        // GET: Recruiters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        // POST: Recruiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Recruiter recruiter = db.Recruiters.Find(id);
            db.Recruiters.Remove(recruiter);
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

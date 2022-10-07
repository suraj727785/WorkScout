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
    public class JobApplicantsController : Controller
    {
        private WorkScoutEntities db = new WorkScoutEntities();

        // GET: JobApplicants
        public ActionResult Index()
        {
            var jobApplicants = db.JobApplicants.Include(j => j.UserDetail).Include(j => j.JobPost);
            return View(jobApplicants.ToList());
        }

        // GET: JobApplicants/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            return View(jobApplicant);
        }

        // GET: JobApplicants/Create
        public ActionResult Create()
        {
            ViewBag.applicantEmail = new SelectList(db.UserDetails, "Email", "Name");
            ViewBag.JobId = new SelectList(db.JobPosts, "JobId", "RecruiterEmail");
            return View();
        }

        // POST: JobApplicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,applicantEmail,CStatus")] JobApplicant jobApplicant)
        {
            if (ModelState.IsValid)
            {
                db.JobApplicants.Add(jobApplicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.applicantEmail = new SelectList(db.UserDetails, "Email", "Name", jobApplicant.applicantEmail);
            ViewBag.JobId = new SelectList(db.JobPosts, "JobId", "RecruiterEmail", jobApplicant.JobId);
            return View(jobApplicant);
        }

        // GET: JobApplicants/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicantEmail = new SelectList(db.UserDetails, "Email", "Name", jobApplicant.applicantEmail);
            ViewBag.JobId = new SelectList(db.JobPosts, "JobId", "RecruiterEmail", jobApplicant.JobId);
            return View(jobApplicant);
        }

        // POST: JobApplicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,applicantEmail,CStatus")] JobApplicant jobApplicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobApplicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.applicantEmail = new SelectList(db.UserDetails, "Email", "Name", jobApplicant.applicantEmail);
            ViewBag.JobId = new SelectList(db.JobPosts, "JobId", "RecruiterEmail", jobApplicant.JobId);
            return View(jobApplicant);
        }

        // GET: JobApplicants/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            return View(jobApplicant);
        }

        // POST: JobApplicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            db.JobApplicants.Remove(jobApplicant);
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

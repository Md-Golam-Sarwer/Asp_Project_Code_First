using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeInformationSystem.Models;

namespace EmployeeInformationSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
      
            return View(db.Departments.ToList());
        }

        


        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName");
            return View();
        }


        [HttpPost]

        public ActionResult Create( Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", department.BranchID);
            return View(department);
        }


        public ActionResult Edit(int id=0)
        {
         
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", department.BranchID);
            return View(department);
        }


        [HttpPost]

        public ActionResult Edit( Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", department.BranchID);
            return View(department);
        }


        public ActionResult Delete(int id=0)
        {

            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id=0)
        {

            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
    }
}

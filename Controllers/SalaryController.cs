using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmployeeInformationSystem.Models;
using EmployeeInformationSystem.Models.ViewModels;


namespace EmployeeInformationSystem.Controllers
{
    
    public class SalaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Salary
        public ActionResult Index()
        {
            return View(db.Salaries.ToList());
        }

        public ActionResult Add()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SalaryVM salaryVM)
        {
            Salary salary = new Salary();
            if (ModelState.IsValid)
            {
                salary.EmployeeID = salaryVM.EmployeeID;
                salary.BasicSalary = salaryVM.BasicSalary;
                salary.HouseRent = salaryVM.HouseRent;
                salary.IsActive = salaryVM.IsActive;

                db.Salaries.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName", salaryVM.EmployeeID);
            //Session["Ename"] = ViewBag.EmployeeID.
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Salary salary = db.Salaries.SingleOrDefault(s => s.SalaryID == id);
            var salaryVm = new SalaryVM();
            salaryVm.EmployeeID = salary.EmployeeID;
            salaryVm.BasicSalary = salary.BasicSalary;
            salaryVm.HouseRent = salary.HouseRent;
            salaryVm.IsActive = salary.IsActive;
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            return View(salaryVm);
            


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalaryVM salaryVM,int id)
        {
            Salary salary = db.Salaries.Find(id);
            if (ModelState.IsValid)
            {
                salary.EmployeeID = salaryVM.EmployeeID;
                salary.BasicSalary = salaryVM.BasicSalary;
                salary.HouseRent = salaryVM.HouseRent;
                salary.IsActive = salaryVM.IsActive;

                db.Entry(salary).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName", salaryVM.EmployeeID);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            Salary salary = db.Salaries.SingleOrDefault(s => s.SalaryID == id);
            var salaryVM = new SalaryVM();
            salaryVM.EmployeeID = salary.EmployeeID;
            salaryVM.BasicSalary = salary.BasicSalary;
            salaryVM.HouseRent = salary.HouseRent;
            salaryVM.IsActive = salary.IsActive;
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            return View(salaryVM);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(SalaryVM salaryVM, int id)
        {

            Salary salary = db.Salaries.Find(id);
            if (salary != null)
            {
                db.Salaries.Remove(salary);



                db.SaveChanges();

            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            return RedirectToAction("Index");

        }
        //public ActionResult Details(int id)
        //{

        //    Salary salary = db.Salaries.SingleOrDefault(s => s.SalaryID == id);
        //    var salaryVM = new SalaryVM();
        //    salaryVM.EmployeeID = salary.EmployeeID;
        //    salaryVM.BasicSalary = salary.BasicSalary;
        //    salaryVM.HouseRent = salary.HouseRent;
        //    salaryVM.IsActive = salary.IsActive;
        //    ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeName");
            
            
            //return View(salaryVM);

        //}
    }
}
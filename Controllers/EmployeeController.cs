﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EmployeeInformationSystem.Models;

namespace EmployeeInformationSystem.Models
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult GetDepartmentList (int branchId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Department> departmentList = db.Departments.Where(d => d.BranchID == branchId).ToList();
            return Json(departmentList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "EmployeeName:_desc" : "";
            var emp = from d in db.Employees
                         select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(d => d.EmployeeName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "EmployeeName:_desc":
                    emp = emp.OrderByDescending(d => d.EmployeeName);
                    break;
                default:
                    emp = emp.OrderBy(d => d.EmployeeName);
                    break;
            }
            ViewBag.BranchList = new SelectList(db.Branches, "BranchID", "BranchName");
            return View(emp);
        }



        public ActionResult Create()
        {
            ViewBag.BranchList = new SelectList(db.Branches, "BranchID", "BranchName");
            ViewBag.DepartmentName = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee em)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(em.UploadImage.FileName);
                string extension = Path.GetExtension(em.UploadImage.FileName);
                HttpPostedFileBase postedFile = em.UploadImage;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        em.EmployeeImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        em.UploadImage.SaveAs(fileName);
                        db.Employees.Add(em);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Inserted Successfully !!!')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Not Inserted !!!')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size Should be less than 1MB !!!')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported!!!')</script>";
                }
            }
            ViewBag.BranchList = new SelectList(db.Branches, "BranchID", "BranchName", em.BranchID);
            ViewBag.DepartmentName = new SelectList(db.Departments, "DepartmentID", "DepartmentName", em.DepartmentID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            var EmployeeRow = db.Employees.Where(t => t.EmployeeID == id).FirstOrDefault();
            Session["Image"] = EmployeeRow.EmployeeImage;
            ViewBag.BranchList = new SelectList(db.Branches, "BranchID", "BranchName");
            ViewBag.DepartmentName = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View(EmployeeRow);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(employee.UploadImage.FileName);
                    string extension = Path.GetExtension(employee.UploadImage.FileName);
                    HttpPostedFileBase postedFile = employee.UploadImage;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            employee.EmployeeImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            employee.UploadImage.SaveAs(fileName);
                            db.Entry(employee).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["CreateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Employee");
                            }
                            else
                            {
                                TempData["CreateMessage"] = "<script>alert('Record Not Updated!!')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size Should Be Less Then 1 MB!!')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported!!')</script>";
                    }
                }
                else
                {
                    employee.EmployeeImage = Session["Image"].ToString();
                    db.Entry(employee).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully!!')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Not Updated!!')</script>";
                    }
                }
            }
            ViewBag.BranchList = new SelectList(db.Branches, "BranchID", "BranchName", employee.BranchID);
            ViewBag.DepartmentName = new SelectList(db.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View();
        }
        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                HttpNotFound();
            }
            return View(employee);
        }
    }
}

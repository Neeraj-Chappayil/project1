using project1.Context;
using project1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace project1.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeContext db;
        public EmployeesController()
        {
            db = new EmployeeContext();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index", "Employees");
        }
        
        public ActionResult Index()
        {
            var employees = db.Employees.ToList(); 
            return View(employees);
        }
        public ActionResult Details(int id)
        {
            var employee = db.Employees.SingleOrDefault(a => a.Id == id);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(employee);
        }


    }
}
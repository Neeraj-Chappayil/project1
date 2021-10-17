using project1.Context;
using project1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project1.Controllers.Api
{
    public class EmployeesController : ApiController
    {
        private EmployeeContext db;
        public EmployeesController()
        {
            db = new EmployeeContext();
        }
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if(employee==null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public IHttpActionResult CreateEmployee(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Employees.Add(employee);
            db.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + employee.Id), employee);
        }
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id,Employee employee)
        {
            if (!ModelState.IsValid)
            
                return BadRequest(ModelState);
            var employeeInDb = db.Employees.SingleOrDefault(a => a.Id == id);
            if (employeeInDb == null)
                return NotFound();
            employeeInDb.Name = employee.Name;
            employeeInDb.Department = employee.Department;
            db.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employeeInDb = db.Employees.SingleOrDefault(a => a.Id == id);
            if (employeeInDb == null)
                return NotFound();
            db.Employees.Remove(employeeInDb);
            db.SaveChanges();
            return Ok();
        }


    }
}

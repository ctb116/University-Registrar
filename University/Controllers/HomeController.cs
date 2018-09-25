using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University;
using System;

namespace University.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpPost("/AddStudent")]
      public ActionResult AddStudent(string name, string date)
      {
        Student newStudent = new Student(name, Convert.ToDateTime(date));
        newStudent.Save();
        return RedirectToAction("Index");
      }

      [HttpPost("/AddDepartment")]
      public ActionResult AddDepartment(string name, int budget)
      {
        Department newDepartment = new Department(name, budget);
        newDepartment.Save();
        return RedirectToAction("Index");
      }

      [HttpPost("/AddCourse")]
      public ActionResult AddCourse(string name, string catalog, int cost, int deptID)
      {
        Course newCourse = new Course(name, catalog, cost, deptID);
        newCourse.Save();
        return RedirectToAction("Index");
      }

      [HttpPost("/AddRegistrar")]
      public ActionResult AddRegistrar(int studentID, int courseID, int deptID)
      {
        Registrar newRegistrar = new Registrar(studentID, courseID, deptID);
        newRegistrar.Save();
        return RedirectToAction("Index");
      }

      [HttpGet("/details/{id}")]
      public ActionResult ViewDetails(int id)
      {
        Registrar foundRegistrar = Registrar.FindWithStudent(id);
        return View("Details", foundRegistrar);
      }
    }
}

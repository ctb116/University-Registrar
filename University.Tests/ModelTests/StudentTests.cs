using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Models;
using System;

namespace University.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnsStudentsInList_1()
    {
      Student newStudent = new Student("Barry", Convert.ToDateTime("2018-02-02"));
      newStudent.Save();

      int results = Student.GetAll().Count;

      Assert.AreEqual(1, results);
    }

    [TestMethod]
    public void Delete_ReturnsStudentsInList_0()
    {
      Student newStudent = new Student("Barry", Convert.ToDateTime("2018-02-02"));
      newStudent.Save();
      newStudent.Delete();

      int results = Student.GetAll().Count;

      Assert.AreEqual(0, results);
    }
    [TestMethod]
    public void DeleteAll_ReturnsStudentsInList_0()
    {
      Student newStudent = new Student("Barry", Convert.ToDateTime("2018-02-02"));
      newStudent.Save();
      Student.DeleteAll();

      int results = Student.GetAll().Count;

      Assert.AreEqual(0, results);
    }

    [TestMethod]
    public void Find_ReturnsStudent_Name()
    {
      Student newStudent = new Student("Barry", Convert.ToDateTime("2018-02-02"));
      newStudent.Save();

      string results = Student.Find(newStudent.Id).Name;

      Assert.AreEqual("Barry", results);
    }


  }
}

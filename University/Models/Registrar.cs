using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
  public class Registrar
  {
    public int Id { get; set; }
    public int Student_Id { get; set; }
    public int Course_Id { get; set; }
    public int Dept_Id {get; set; }

    public Registrar (int student_id, int course_id, int dept_id, int id = 0)
    {
      Id = id;
      Student_Id = student_id;
      Dept_Id = dept_id;
      Course_Id = course_id;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO registrar (student_id, course_id, dept_id) VALUES (@student, @course, @dept);";
      cmd.Parameters.AddWithValue("@student", this.Student_Id);
      cmd.Parameters.AddWithValue("@course", this.Course_Id);
      cmd.Parameters.AddWithValue("@dept", this.Dept_Id);

      Console.WriteLine("courseID " + this.Course_Id);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Registrar> GetAll()
    {
      List<Registrar> allRegistrars = new List<Registrar> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM registrar;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int registrarId = rdr.GetInt32(0);
        int registrarStudent = rdr.GetInt32(1);
        int registrarCourse = rdr.GetInt32(2);
        int registrarDept = rdr.GetInt32(3);

        Registrar newRegistrar = new Registrar(registrarStudent, registrarCourse, registrarDept ,registrarId);
        allRegistrars.Add(newRegistrar);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRegistrars;
    }

    // public static List<Registrar> GetAll()
    // {
    //   List<Registrar> allRegistrars = new List<Registrar> {};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM registrar JOIN students ON (student_id = students.id) JOIN courses ON (course_id = courses.id) JOIN departments ON (departments.id = registrar.dept_id);";
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   while(rdr.Read())
    //   {
    //     int registrarId = rdr.GetInt32(0);
    //     int registrarStudent = rdr.GetInt32(1);
    //     int registrarCourse = rdr.GetInt32(2);
    //     int registrarDept = rdr.GetInt32(3);
    //
    //     Registrar newRegistrar = new Registrar(registrarStudent, registrarCourse, registrarDept ,registrarId);
    //     allRegistrars.Add(newRegistrar);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return allRegistrars;
    // }


    public static Registrar Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM registrar WHERE id = @searchId;";
      // JOIN students ON (students.id = registrar.student_id) WHERE registrar.id = @searchId;"
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundID = 0;
      int studentID = 0;
      int courseID = 0;
      int deptID = 0;

      while(rdr.Read())
      {
        foundID = rdr.GetInt32(0);
        studentID = rdr.GetInt32(1);
        courseID = rdr.GetInt32(2);
        deptID = rdr.GetInt32(3);
      }

      Registrar foundRegistrar = new Registrar(studentID, courseID, deptID, foundID);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRegistrar;
    }

    public static List<Registrar> FindAll(int id)
    {
      List<Registrar> foundClasses = new List<Registrar> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM students JOIN registrar ON (students.id = registrar.student_id) JOIN courses ON (registrar.course_id = courses.id) WHERE students.id = @searchID;";

      cmd.Parameters.AddWithValue("@searchID", 5);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundID = 0;
      int studentID = 0;
      int courseID = 0;
      int deptID = 0;
      Registrar newRegistrar;

      while(rdr.Read())
      {
        foundID = rdr.GetInt32(0);
        studentID = rdr.GetInt32(1);
        courseID = rdr.GetInt32(2);
        deptID = rdr.GetInt32(3);
        newRegistrar = new Registrar(studentID, courseID, deptID, foundID);
        foundClasses.Add(newRegistrar);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClasses;
    }

    public static Registrar FindWithStudent(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM registrar WHERE student_id = @searchId;";
      // JOIN students ON (students.id = registrar.student_id) WHERE registrar.id = @searchId;"
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundID = 0;
      int studentID = 0;
      int courseID = 0;
      int deptID = 0;

      while(rdr.Read())
      {
        foundID = rdr.GetInt32(0);
        studentID = rdr.GetInt32(1);
        courseID = rdr.GetInt32(2);
        deptID = rdr.GetInt32(3);
      }

      Registrar foundRegistrar = new Registrar(studentID, courseID, deptID, foundID);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRegistrar;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM registrar WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", this.Id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM registrar;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}

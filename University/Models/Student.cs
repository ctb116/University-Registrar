using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
  public class Student
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Enrolled { get; set; }
    public int Dept_Id { get; set; }

    public Student(string name, DateTime enrolled, int id = 0)
    {
      Id = id;
      Name = name;
      Enrolled = enrolled;
      Dept_Id = 0;
    }

    public Student(string name, DateTime enrolled, int id, int deptId = 0)
    {
      Id = id;
      Name = name;
      Enrolled = enrolled;
      Dept_Id = deptId;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enroll_date, dept_id) VALUES (@name, @enroll_date, @dept_id);";
      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@enroll_date", this.Enrolled);
      cmd.Parameters.AddWithValue("@dept_id", this.Dept_Id);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentEnrolled = rdr.GetDateTime(2);
        int studentDept = rdr.GetInt32(3);

        Student newStudent = new Student(studentName, studentEnrolled, studentId, studentDept);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStudents;
    }

    

    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundId = 0;
      string name = "";
      DateTime enrolled = DateTime.MinValue;
      int dept = 0;
      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        enrolled = rdr.GetDateTime(2);
        dept = rdr.GetInt32(3);
      }

      Student foundStudent = new Student(name, enrolled, foundId, dept);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStudent;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students WHERE id = @searchId;";
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
      cmd.CommandText = @"DELETE FROM students;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
  public class Course
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Catalog { get; set; }
    public int Cost { get; set; }
    public int Dept_Id { get; set; }

    public Course (string name, string catalog, int cost, int dept_id, int id = 0)
    {
      Id = id;
      Name = name;
      Catalog = catalog;
      Cost = cost;
      Dept_Id = dept_id;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses (name, course_number, dept_id, cost) VALUES (@name, @course_number, @dept_id, @cost);";
      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@course_number", this.Catalog);
      cmd.Parameters.AddWithValue("@dept_id", this.Dept_Id);
      cmd.Parameters.AddWithValue("@cost", this.Cost);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseCatalog = rdr.GetString(2);
        int courseCost = rdr.GetInt32(3);
        int courseDeptId = rdr.GetInt32(4);

        Course newCourse = new Course(courseName, courseCatalog, courseCost, courseDeptId, courseId);
        allCourses.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
    }

    public static Course Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      rdr.Read();

      int foundId = rdr.GetInt32(0);
      string name = rdr.GetString(1);
      string catalog = rdr.GetString(2);
      int cost = rdr.GetInt32(3);
      int dept_id = rdr.GetInt32(4);

      Course foundCourse = new Course(name, catalog, cost, dept_id, foundId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCourse;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses WHERE id = @searchId;";
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
      cmd.CommandText = @"DELETE FROM courses;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}

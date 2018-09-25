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

    public Registrar (int student_id, int course_id, int id = 0)
    {
      Id = id;
      Student_Id = student_id;
      Course_Id = course_id;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO registrar (student_id, course_id) VALUES (@student, @course;";
      cmd.Parameters.AddWithValue("@student", this.Student_Id);
      cmd.Parameters.AddWithValue("@course", this.Course_Id);

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

        Registrar newRegistrar = new Registrar(registrarStudent, registrarCourse, registrarId);
        allRegistrars.Add(newRegistrar);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRegistrars;
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

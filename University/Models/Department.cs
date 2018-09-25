using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using University;

namespace University.Models
{
  public class Department
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Budget { get; set; }

      public Department(string name, int budget, int id = 0)
      {
        Id = id;
        Name = name;
        Budget = budget;
      }
      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO departments (name, budget) VALUES (@name, @budget);";
        cmd.Parameters.AddWithValue("@name", this.Name);
        cmd.Parameters.AddWithValue("@budget", this.Budget);

        cmd.ExecuteNonQuery();
        Id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Department> GetAll()
      {
        List<Department> allDepartments = new List<Department> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM departments;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int departmentId = rdr.GetInt32(0);
          string departmentName = rdr.GetString(1);

          Department newDepartment = new Department(departmentName, departmentId);
          allDepartments.Add(newDepartment);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allDepartments;
      }

      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM departments WHERE id = @searchId;";
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
        cmd.CommandText = @"DELETE FROM departments;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
  }
}

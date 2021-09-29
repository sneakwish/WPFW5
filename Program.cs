using System;
using Microsoft.EntityFrameworkCore;

namespace wpfwopdracht5
{
public class Grade {
public int Id { get; set; }
public int Value { get; set; }
}
public class Student {
public int Id { get; set; }
public string Naam { get; set; }
// public List<Grade> Grades { get; set; }
}

public class MyContext : DbContext {
protected override void OnConfiguring(DbContextOptionsBuilder b) =>
b.UseSqlite("Data Source=database.db");
public DbSet<Student> Studenten { get; set; }
}
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyContext c = new MyContext();
            c.Studenten.Add(new Student(){ Id = 456, Naam = "Bob2"});
            c.SaveChanges();
            //yo
        }
    }
}

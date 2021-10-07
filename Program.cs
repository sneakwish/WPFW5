using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace wpfwopdracht5
{
public class Grade {
    public int Id { get; set; }
    public int Value { get; set; }
    public int StudentId { get; set; }

    public Student Student { get; set; }

}
public class Student {
    public int Id { get; set; }
    [Required]
    public string Naam { get; set; }
    public Grade Grade { get; set; }


    // public List<Grade> Grades { get; set; }
}

public class MyContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder b) =>
    b.UseSqlite("Data Source=database.db");
    public DbSet<Student> Studenten { get; set; }
    public DbSet<Grade> cijfers{get; set;}
}
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyContext c = new MyContext();
            c.Studenten.Add(new Student(){Naam = "bole"});
            c.Studenten.Add(new Student(){Naam = "bole"});
            c.Studenten.Add(new Student(){Naam = "bole"});
            c.Studenten.Add(new Student(){Naam = "bole"});
            c.Studenten.Add(new Student(){Naam = "bole"});
    
            // c.Studenten.Single((s) => s.Id == 123).Grades.Add(new Grade);
          
            c.SaveChanges();
            
            // Student st;
            // var wishal = c.Studenten.Single(s=>s.Naam == "Wishal");
            // c.Entry(wishal).Collection(st => st.Id).Load();
            // Console.WriteLine(wishal);
            //yofjfh
        }
    }
}

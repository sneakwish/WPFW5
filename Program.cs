using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace wpfwopdracht5
{
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
public class MyContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder b) =>
    b.UseSqlite("Data Source=database.db");
    public DbSet<Blog> Studenten { get; set; }
    public DbSet<Post> cijfers{get; set;}
}
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyContext c = new MyContext();
            // c.Studenten.Add(new Student(){Naam = "bole"});
            // c.Studenten.Add(new Student(){Naam = "bole"});
            // c.Studenten.Add(new Student(){Naam = "bole"});
            // c.Studenten.Add(new Student(){Naam = "bole"});
            // c.Studenten.Add(new Student(){Naam = "bole"});
    
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

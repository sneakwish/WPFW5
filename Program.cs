using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ORM
{
    class Verhuurder
    {
        [Required] public string Name { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Mobile { get; set; }
        [Key] public string Email { get; set; }
        public List<Auto> Autos { get; set; }
    }

    class Huurder
    {
        [Required] public string Name { get; set; }
        [Required] public string Mobile { get; set; }
        [Key] public string Email { get; set; }
    }

    class Auto
    {
        public string VerhuurderEmail { get; set; }
        
        [ForeignKey("VerhuurderEmail")]
        public Verhuurder Verhuurder { get; set; }
        [Required, Key] public string Brand { get; set; }
        public IEnumerable<Period> Periods { get; set; }

        public int BorrowedCount = 0;
    }

    class Period
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required] public DateTime StartTime { get; set; }
        [Required] public TimeSpan BorrowTime { get; set; }

        public bool Available(Period other)
        {
            return StartTime < other.StartTime + other.BorrowTime && other.StartTime < StartTime + BorrowTime;
        }
    }

    class MyDbContext : DbContext
    {
        public DbSet<Verhuurder> Verhuurders { get; set; }
        public DbSet<Huurder> Huurders { get; set; }

        public DbSet<Auto> Autos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder b)
        {
            b.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Verhuurder>().HasData(new Verhuurder
                {
                    Name = "kevin",
                    Address = "beverstraat 25",
                    Email = "kevin@gmail.com",
                    Mobile = "063752655",
                },
                new Verhuurder
                {
                    Name = "Wishal",
                    Address = "kalrestraat 32",
                    Email = "Wishal@gmail.com",
                    Mobile = "063752652",
                }
            );

            modelBuilder.Entity<Auto>().HasData(
                new Auto
                {
                    VerhuurderEmail = "kevin@gmail.com",
                    Brand = "Tesla"
                },
                new Auto
                {
                    VerhuurderEmail = "Wishal@gmail.com",
                    Brand = "Volkswagen"
                });

            modelBuilder.Entity<Huurder>().HasData(new Huurder
                {
                    Email = "huurman@gmail.com",
                    Mobile = "0638952354",
                    Name = "Huurman1"
                },
                new Huurder
                {
                    Email = "buurman@gmail.com",
                    Mobile = "063852685",
                    Name = "buurman"
                });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        
            var context = new MyDbContext();
            Console.WriteLine("Wat is uw mail?");
            var email = Console.ReadLine();
            var verhuurder = context.Verhuurders.FirstOrDefault(x => x.Email == email);

            while(verhuurder == null){
                System.Console.WriteLine("Verhuurder onbekend");
                Console.WriteLine("Wat is uw mail?");
                email = Console.ReadLine();
                verhuurder = context.Verhuurders.FirstOrDefault(x => x.Email == email);
            }
           
            System.Console.WriteLine("Hallo " + verhuurder.Name);
            System.Console.WriteLine("Welke auto wilt u toevoegen?");

            var auto = Console.ReadLine();
            if(auto != "geen"){
            context.Autos.Add(new Auto {Brand = auto, VerhuurderEmail = verhuurder.Email});
            context.SaveChanges();
            System.Console.WriteLine("De Auto is toegevoegd!");
            }
            System.Console.WriteLine("Geen auto toegevoegd.");


            System.Console.WriteLine("Wat is uw email?");
            var email2 = Console.ReadLine();
            var huurder = context.Huurders.FirstOrDefault(x => x.Email == email2);
            while(huurder == null){
                System.Console.WriteLine("Huurder onbekend");
                Console.WriteLine("Wat is uw mail?");
                email2 = Console.ReadLine();
                huurder = context.Huurders.FirstOrDefault(x => x.Email == email2);
            }
            System.Console.WriteLine("Wanneer wil je een auto huren? (DD-MM-YYYY)");
            var startTime = Console.ReadLine();
            var dateTime = new DateTime();
            Boolean dateParse = DateTime.TryParse(startTime, out dateTime);
            while(!dateParse){
                System.Console.WriteLine("Foute Notatie! (DD-MM-YYYY)");
                startTime = Console.ReadLine();
                dateTime = new DateTime();
                dateParse = DateTime.TryParse(startTime, out dateTime);
            }
            System.Console.WriteLine("Je wilt huren op: "+ startTime);
            Console.WriteLine("Voor hoeveel uur wilt u de auto huren?");
            double hoeveel = Convert.ToDouble(Console.ReadLine());
            var uren = TimeSpan.FromHours(hoeveel);

            var periode = new Period {
                            StartTime = dateTime,
                            BorrowTime = uren
                        };

            System.Console.WriteLine("Welke auto wil je huren?");
            var cars = context.Autos.Include(x=>x.Periods)
            .Where(x => x.Periods.All(t=>t.Available)
Periods.All(y => y.Available(periode)));
            foreach(var ga in cars){
                System.Console.WriteLine(ga.Brand);
            }
            var keuze = Console.ReadLine();           
            var autoQuery = cars.FirstOrDefault(x => x.Brand == keuze);
            autoQuery.Periods.ToList().Add(periode);
            context.SaveChanges();

            System.Console.WriteLine("U hebt geboekt!");
            


            // while (true)
            // {

            //     var contextVerhuurders = context.Verhuurders;
            //     var verhuurder = context.Verhuurders.FirstOrDefault(x => x.Email == email);
            //     if (verhuurder != null)
            //     {
            //         Console.WriteLine("Welke auto wilt u toevoegen?");
            //         var auto = Console.ReadLine();
            //         verhuurder.Autos.ToList().Add(new Auto { Brand = auto, VerhuurderEmail = verhuurder.Email});
            //         context.SaveChanges();
            //     }
            //     else
            //     {
            //         var huurder = context.Huurders.FirstOrDefault(x => x.Email == email);
            //         if (huurder != null)
            //         {
            //             Console.WriteLine("Wanneer wilt u een auto lenen?  (YYYY-MM-DD 00:00:00)");
            //             var startTime = Console.ReadLine();
            //             var dateTime = new DateTime();
            //             var Succes = DateTime.TryParse(startTime, out dateTime);
            //             Console.WriteLine("Voor hoeveel uur wilt u de auto huren?");
            //             var duration = Console.ReadLine();
            //             var durationDouble = double.Parse(duration);
            //             var borrowTime = TimeSpan.FromHours(durationDouble);

                        // var period = new Period
                        // {
                        //     StartTime = dateTime,
                        //     BorrowTime = borrowTime
                        // };
            //             var autosVrij = context.Autos.Include(x => x.Periods)
            //                 .Where(x => x.Periods.All(y => y.Available(period)));
            //             Console.WriteLine($"Dit zijn de opties voor u: {string.Join(",", autosVrij)}. Welke wilt u?");
            //             var choice = Console.ReadLine();
            //             var gekozenAuto = autosVrij.FirstOrDefault(x => x.Brand == choice);
            //             gekozenAuto.Periods.ToList().Add(period);

            //             context.SaveChanges();
            //         }
            //         else
            //         {
            //             Console.WriteLine("Gebruiker niet gevonden");
            //         }
            //     }
            // }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Meow.Code.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Meow.Code.DAL
{
    public class MeowContext : DbContext, IMeowContext
    {
        public MeowContext() : base("MeowContext")
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<MeowMessage> Meows { get; set; }

        public void AddCat(Cat cat)
        {
            Cats.Add(cat);
        }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //one-to-many 
         
            modelBuilder.Entity<MeowMessage>()
                     .HasRequired<Cat>(s => s.Cat)
                    .WithMany(s => s.Meows);
        

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Meow.Code.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Meow.Code.DAL
{
    public class MeowContext : DbContext
    {
        public MeowContext() : base("MeowContext")
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Meow> meows { get; set; }
        public DbSet<Topic> topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //one-to-many 
            modelBuilder.Entity<Topic>()
                        .HasRequired<Cat>(s => s.Cat)
                        .WithMany(s => s.tpics);
            modelBuilder.Entity<Meow>()
                      .HasRequired<Cat>(s => s.Cat)
                      .WithMany(s => s.meows);
            modelBuilder.Entity<Meow>()
                    .HasRequired<Topic>(s => s.Topic)
                    .WithMany(s => s.meows);

        }

    }
}
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
        public DbSet<MeowMessage> Meows { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //one-to-many 
            //modelBuilder.Entity<Topic>()
            //            .HasRequired<Cat>(s => s.Owner);
            //modelBuilder.Entity<Meow>()
            //          .HasRequired<Cat>(s => s.Cat)
            //          .WithMany(s => s.Meows);
            //modelBuilder.Entity<Meow>()
            //        .HasRequired<Topic>(s => s.Topic)
            //        .WithMany(s => s.meows);

        }

    }
}
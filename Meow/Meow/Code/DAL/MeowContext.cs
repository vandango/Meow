using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Meow.Code.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using log4net;
using System.Data.Entity.Infrastructure;

namespace Meow.Code.DAL
{
    public class MeowContext : DbContext, IMeowContext
    {
        private static readonly ILog LOG = LogManager.GetLogger("MeowContext");


        public MeowContext() : base("MeowContext")
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<MeowMessage> Meows { get; set; }

        public void AddCat(Cat cat)
        {
            Cats.Add(cat);
        }

        public Cat Find(long id)
        {
            return Cats.Find(id);
        }

        public void Save()
        {
            try
            {
                this.SaveChanges();
            } catch (DbUpdateException e)
            {
                throw new AccountException(e.Message, e);
            }
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
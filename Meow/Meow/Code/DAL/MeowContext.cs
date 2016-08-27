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
        private static readonly ILog LOG = LogManager.GetLogger(typeof(MeowContext));

        public MeowContext() : base("MeowContext")
        {
        }

        public DbSet<Cat> Kitties { get; set; }
        public DbSet<MeowMessage> MeowMessages { get; set; }
        public DbSet<Follower> Followers { get; set; }

        public DbSet<Follower> Follower()
        {
            return Followers;
        }

        public DbSet<MeowMessage> Meows()
        {
            return MeowMessages;
        }

        public DbSet<Cat> Cats()
        {
            return Kitties;
        }

        public void AddCat(Cat cat)
        {
            Kitties.Add(cat);
        }

        public Cat Find(long id)
        {
            return Kitties.Find(id);
        }

        public Cat FindByCredentials(string username, string password)
        {
            var foundCats = Kitties.Where(c => c.Username == username && c.Password == password).ToList();
            Cat loggedInCat = null;
            if (foundCats.Count == 1)
            {
                loggedInCat = foundCats.ElementAt(0);
            }

            return loggedInCat;
        }

        public void Save()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new AccountCreationException(e.Message, e);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //one-to-many 
         
            modelBuilder.Entity<MeowMessage>().HasRequired<Cat>(s => s.Cat).WithMany(s => s.Meows);
            //modelBuilder.Entity<Follower>().HasRequired<Cat>(f => f.IsFollowing).WithMany(f => f.Follower);
            //modelBuilder.Entity<Follower>().HasRequired<Cat>(f => f.IsBeingFollowed).WithMany(f => f.Follower);

        }

    }
}
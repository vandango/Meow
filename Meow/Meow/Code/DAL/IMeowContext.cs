using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meow.Code.Model;
using System.Data.Entity;

namespace Meow.Code.DAL
{
    public interface IMeowContext
    {
        void AddCat(Cat cat);
        void Save();
        Cat Find(long id);
        Cat FindByCredentials(string username, String password);
        Cat FindByUsername(string username);
        DbSet<Follower> Follower();
        DbSet<MeowMessage> Meows();
        DbSet<Cat> Cats();
        int SaveChanges();
    }
}

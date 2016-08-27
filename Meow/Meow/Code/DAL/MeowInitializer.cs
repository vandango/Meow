using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meow.Code.Model;

namespace Meow.Code.DAL
{
    public class MeowInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MeowContext>
    {
        protected override void Seed(MeowContext context)
        {
            var cats = new List<Cat>()
            {
                new Cat() { Email = "jnaumann@mail.com", Password = "jnaumann", Username = "jnaumann", Created = DateTime.Now},
                new Cat() { Email = "fweichand@mail.com", Password = "fweichand", Username = "fweichand", Created = DateTime.Now },
                new Cat() { Email = "mkoschier@mail.com", Password = "mkoschier", Username = "mkoschier", Created = DateTime.Now },
                new Cat() { Email = "mosdoba@mail.com", Password = "mosdoba", Username = "mosdoba", Created = DateTime.Now },
                new Cat() { Email = "amastouri@mail.com", Password = "amastouri", Username = "amastouri", Created = DateTime.Now },
                new Cat() { Email = "mhinz@mail.com", Password = "mhinz", Username = "mhinz", Created = DateTime.Now },
                new Cat() { Email = "chuff@mail.com", Password = "chuff", Username = "chuff", Created = DateTime.Now }
            };
            cats.ForEach(cat => context.Cats().Add(cat));
            context.SaveChanges();
            foreach(var cat in cats)
            {
                var meowMessages = new List<MeowMessage>()
                {
                    new MeowMessage() {Cat = cat, Created = DateTime.Now, Text = "1st Meow message from " + cat.Username},
                    new MeowMessage() {Cat = cat, Created = DateTime.Now, Text = "2nd Meow message from " + cat.Username},
                    new MeowMessage() {Cat = cat, Created = DateTime.Now, Text = "3rd Meow message from " + cat.Username},
                    new MeowMessage() {Cat = cat, Created = DateTime.Now, Text = "4th Meow message from " + cat.Username},
                    new MeowMessage() {Cat = cat, Created = DateTime.Now, Text = "5th Meow message from " + cat.Username}
                };
            }
            context.SaveChanges();
        }
    }
}
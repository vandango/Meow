using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Meow.Code.Model
{
    public class Cat
    {
        public long Id { get; set; }

        [MaxLength(64)]
        [Index("IX_Username", IsUnique = true)]
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
        public virtual List<MeowMessage> Meows { get; set; }
        public virtual List<Follower> Follower { get; set; }

    }
}
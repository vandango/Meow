using Meow.Code.Model;
using System;
using System.Collections.Generic;

namespace Meow.Models.Profile
{
    public class ProfileCatModel
    {
        public string Username { get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Cat> CatsFollowing { get; set; }
    }
}
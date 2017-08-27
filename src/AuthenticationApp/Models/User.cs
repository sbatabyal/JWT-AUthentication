using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationApp.Models
{
    public class User
    {
        public Guid ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        
    }

    public static class UserStorage
    {
        public static List<User> Users { get; set; } = new List<User> {
            new User {ID=Guid.NewGuid(),Email="user1@test.com",Password = "user1psd", Name = "Test User1" },
            new User {ID=Guid.NewGuid(),Email="user2@test.com",Password = "user2psd", Name = "Test User2" },
            new User {ID=Guid.NewGuid(),Email="user3@test.com",Password = "user3psd", Name = "Test User3%" }
        };
    }
}

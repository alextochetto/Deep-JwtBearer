using System.Collections.Generic;
using System.Linq;
using Api.Models;

namespace Api.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User{ Id = 1, Username = "Alex T", Password = "1234567890@", Role = "Admin" });
            users.Add(new User{ Id = 2, Username = "Luiz Bauer", Password = "@1234567890", Role = "Manager" });
            return users.Where(_ => _.Username.ToLower() == username.ToLower() && _.Password == password).FirstOrDefault();
        }
    }
}
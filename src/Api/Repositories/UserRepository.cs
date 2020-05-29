using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;

namespace Api.Repositories
{
    public static class UserRepository
    {
        public static UserModel Get(string username, string password)
        {
            var users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Username = "Alex T",
                    Password = "1234567890@",
                    Role = "Admin",
                    Feature = "Create"
                },
                new UserModel
                {
                    Id = 2,
                    Username = "Luiz B",
                    Password = "@1234567890",
                    Role = "Manager"
                }
            };
            return users.FirstOrDefault(_ =>
                string.Equals(_.Username, username, StringComparison.CurrentCultureIgnoreCase) &&
                _.Password == password);
        }
    }
}
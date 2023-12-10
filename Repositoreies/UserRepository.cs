using System.Collections.Generic;
using System.Linq;


using ApiAuthSaude.Models;


namespace ApiAuthSaude.Models
{
    public static class UserRepository
    {
        public static User? Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "mestre", Password = "mestre", Role = "manager" },
                new User { Id = 2, Username = "slave", Password = "slave", Role = "employee" }
            };

            return users.FirstOrDefault(x => x.Username == username && x.Password == password) ?? null;
        }
    }
}

//Repositorio de testes.
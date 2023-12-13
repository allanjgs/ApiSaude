ousing System.Collections.Generic;
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
                new User { Id = 1, Username = "medico", Password = "medico", Role = "manager" },
                new User { Id = 2, Username = "paciente", Password = "paciente", Role = "employee" }
            };

            return users.FirstOrDefault(x => x.Username == username && x.Password == password) ?? null;
        }

        internal static object Get(object userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}

//Repositorio de testes.

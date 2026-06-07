using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();

        public User? GetUserById(int id);

        public void AddUser(User user);

        public void UpdateUser(User user);

        public void DeleteUser(User user);

        public bool UserExists(string username, string password);
    }
}

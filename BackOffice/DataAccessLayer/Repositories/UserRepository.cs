using DataAccessLayer.Interfaces;
using BackOffice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MatrixIncDbContext _context;

        public UserRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Orders);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool UserExists(string username, string password)
        {
            return _context.Users.Any(u => u.Name == username && u.Password == password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;
using SimpleRESTApi.Data;
using SimpleRESTApi.Helpers;
using Simple_API.DTO;

namespace SimpleRESTApi.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDBContext _context;
        public AspUserEF(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public AspUser DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }

            var user = _context.AspUsers.Find(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"This '{username}' can't be found.");
            }

            _context.AspUsers.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            return _context.AspUsers.ToList();
        }

        public AspUser GetUserByUsername(string username)
        {
            var user = _context.AspUsers.Find(username);
            if (user == null)
                throw new Exception("User not found");
            return user;
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        throw new ArgumentException("Username and password are required");
    }

            var hashedPassword = HashHelper.HashPassword(password);

    return _context.AspUsers.Any(u =>
        u.Username == username &&
        u.Password == hashedPassword);
        }

        public AspUser RegisterUser(RegisterDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "RegisterDTO cannot be null");
            }

            var user = new AspUser
            {
                Username = dto.Username,
                Password = Helpers.HashHelper.HashPassword(dto.Password),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.Firstname,
                LastName = dto.Lastname,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country
            };

            _context.AspUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        public AspUser UpdateUser(AspUser user)
        {
            throw new NotImplementedException();
        }

        AspUser IAspUser.RegisterUser(AspUser user)
        {
            throw new NotImplementedException();
        }

        void IAspUser.DeleteUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
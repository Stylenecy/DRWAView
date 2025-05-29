using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;

namespace SimpleRESTApi.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser RegisterUser(AspUser user);
        AspUser GetUserByUsername(string username);
        AspUser UpdateUser(AspUser user);
        void DeleteUser(string username);
        bool Login(string username, string password);        
    }
}
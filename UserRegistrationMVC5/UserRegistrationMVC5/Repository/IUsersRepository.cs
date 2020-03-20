using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationMVC5.Models;

namespace UserRegistrationMVC5.Repository
{
    public interface IUsersRepository: IDisposable
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        int AddUser(User userEntity);
        int UpdateUser(User userEntity);
        void DeleteUser(int userId);
    }
}

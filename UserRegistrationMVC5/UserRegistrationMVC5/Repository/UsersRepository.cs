using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using UserRegistrationMVC5.Models;

namespace UserRegistrationMVC5.Repository
{
    public class UsersRepository : IUsersRepository
    {
        [Dependency]
        public UserRegistrationEntities DbContext { get; set; }
        public UsersRepository(UserRegistrationEntities dbContext)
        {

            DbContext = dbContext;
        }
        public int AddUser(User userEntity)
        {
            int result = -1;

            if (userEntity != null)
            {
                DbContext.Users.Add(userEntity);
                DbContext.SaveChanges();
                result = userEntity.Id;
            }
            return result;
        }

        public void DeleteUser(int userId)
        {
            User userEntity = DbContext.Users.Find(userId);
            DbContext.Users.Remove(userEntity);
            DbContext.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return DbContext.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return DbContext.Users.Find(userId);
        }

        public int UpdateUser(User userEntity)
        {
            int result = -1;

            if (userEntity != null)
            {
                DbContext.Entry(userEntity).State = EntityState.Modified;
                DbContext.SaveChanges();
                result = userEntity.Id;
            }
            return result;
        }
    }
}
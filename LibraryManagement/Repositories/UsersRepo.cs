using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories
{
    public class UsersRepo : IUsersRepo
    {
        private readonly ApplicationDbContext db;
        public UsersRepo(ApplicationDbContext d)
        {
            this.db = d;
        }
        public int AddUsers(Users u)
        {
            u.IsActive = 1;
            int result = 0;
            u.RoleID = 2;
            db.Users.Add(u);
            result = db.SaveChanges();
            return result;
        }

        public Users GetUserByEmail(string email)
        {
            return db.Users.Where(x => x.Email == email).SingleOrDefault();
        }

        public Users GetUserById(int id)
        {
            var model = (from u in
                             db.Users
                         where u.UserID == id
                         select u).FirstOrDefault();
            return model;
        }

        public IEnumerable<Users> GetUsers()
        {
            var model = (from u in
                             db.Users
                         select u).ToList();
            return model;
            //var model = (from u in
            //                 db.Users
            //             where u.IsActive == 1
            //             select u).ToList();
            //return model;
        }

        public Users UpdateUserStatus(int userId, int isActive)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                user.IsActive = isActive;
                db.SaveChanges();
            }
            return user;
        }
    }
}

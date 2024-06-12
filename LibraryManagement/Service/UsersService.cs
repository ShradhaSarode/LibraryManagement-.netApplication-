using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Service
{
    public class UsersService: IUsersService
    {
        private readonly IUsersRepo repo;
        public UsersService(IUsersRepo repo)
        {
            this.repo = repo;
        }

        public int AddUsers(Users u)
        {
            return repo.AddUsers(u);
        }

        public Users GetUserByEmail(string email)
        {
            return repo.GetUserByEmail(email);
        }

        public Users GetUserById(int id)
        {
           return repo.GetUserById(id);
        }

        public IEnumerable<Users> GetUsers()
        {
            return repo.GetUsers();
        }

        public Users UpdateUserStatus(int userId, int isActive)
        {
            return repo.UpdateUserStatus(userId,isActive);
        }
    }
}

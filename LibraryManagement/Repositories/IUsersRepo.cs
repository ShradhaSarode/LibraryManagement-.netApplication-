using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IUsersRepo
    {
        IEnumerable<Users> GetUsers();
        Users GetUserById(int id);
        int AddUsers(Users u);
        public Users GetUserByEmail(string email);
        Users UpdateUserStatus(int userId, int isActive);
        //int EditUsers(Users u);
        //int DeleteUsers(int id);
    }
}

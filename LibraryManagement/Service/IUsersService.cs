using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IUsersService
    {
        IEnumerable<Users> GetUsers();
        Users GetUserById(int id);
        int AddUsers(Users u);
        public Users GetUserByEmail(string email);
        Users UpdateUserStatus(int userId, int isActive);
    }
}

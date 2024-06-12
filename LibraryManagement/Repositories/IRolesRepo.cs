using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IRolesRepo
    {
        IEnumerable<Roles> GetRoles();
        Roles GetRoleById(int id);
    }
}

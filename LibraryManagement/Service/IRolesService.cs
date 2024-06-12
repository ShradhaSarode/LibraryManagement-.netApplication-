using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IRolesService
    {
        IEnumerable<Roles> GetRoles();
        Roles GetRoleById(int id);
    }
}

using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Service
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo repo;
        public RolesService(IRolesRepo repo)
        {
            this.repo = repo;
        }
        public Roles GetRoleById(int id)
        {
            return repo.GetRoleById(id);
        }

        public IEnumerable<Roles> GetRoles()
        {
            return repo.GetRoles();
        }
    }
}

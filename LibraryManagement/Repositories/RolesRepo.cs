using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class RolesRepo : IRolesRepo
    {
        private ApplicationDbContext db;
        public RolesRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Roles GetRoleById(int id)
        {
            var model = (from roles in
                             db.Roles
                         where roles.RoleID == id
                         select roles).FirstOrDefault();
            return model;
        }

        IEnumerable<Roles> IRolesRepo.GetRoles()
        {
            var model = (from roles in
                            db.Roles
                         select roles).ToList();
            return model;
        }
    }
}

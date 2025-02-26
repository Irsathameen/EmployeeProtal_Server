using EmployeePortal.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Data.Interface
{
    public interface ILoginRepository
    {
         IEnumerable<User> GetUsers();
        public bool CheckUsersExists(User objUser);
        User GetFirstUsers();
        IEnumerable<UserRole> GetUserRoles();

        bool SaveUsers(User user);
    }
}

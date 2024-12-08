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
        public List<User> GetUsers();
        public bool CheckUsersExists(User objUser);
    }
}

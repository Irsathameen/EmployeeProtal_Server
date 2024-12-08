using EmployeePortal.Data.DataContext;
using EmployeePortal.Data.DataModel;
using EmployeePortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        public readonly EmployeeContext _context;

        public LoginRepository(EmployeeContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool CheckUsersExists(User objUser)
        {
            return _context.Users.Where(obj => obj.Name.Equals(objUser.Name) && obj.Password.Equals(objUser.Password)).Any();

        }
    }
}

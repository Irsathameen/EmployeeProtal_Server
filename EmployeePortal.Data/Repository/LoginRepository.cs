using EmployeePortal.Data.DataContext;
using EmployeePortal.Data.DataModel;
using EmployeePortal.Data.Interface;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<User> GetUsers()
        {
            return _context.T_Users.Include(a=>a.UserRole);
        }
        public User GetFirstUsers()
        {
            return _context.T_Users.SingleOrDefault();
        }
        public bool CheckUsersExists(User objUser)
        {
            return _context.T_Users.Where(obj => obj.Name.Equals(objUser.Name) && obj.Password.Equals(objUser.Password)).Any();

        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _context.T_UserRole;
        }

      public  bool SaveUsers(User user)
        {
            
            if(user != null)
            {

               var result = _context.T_Users.Update(user);
               _context.SaveChanges();

            

                long a = user.UserID;
            }
            return true;
        }
    }

    
    
    }


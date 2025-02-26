using EmployeePortal.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Test.Mock
{
    internal class UserMockData
    {

        public static List<UserRole> GetMockUser()
        {
            return new List<UserRole>()
            {
            new UserRole {  ID=1,Name="Admin"}
        };
        }
    }
}

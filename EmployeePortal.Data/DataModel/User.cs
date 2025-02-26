using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Data.DataModel
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long UserID { get; set; }
      
        public string Name { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("UserRole")]
        public int RoleID { get; set; }
        public virtual UserRole UserRole { get; set; }
      
    }

    public class UserRole
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}

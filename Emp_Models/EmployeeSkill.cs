using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Models
{
    public class EmployeeSkill
    {
        [Key]
        public int Id { get; set; }
        public int EmployeesEmpId { get; set; }
        public int SkillsId { get; set; }
    }
}

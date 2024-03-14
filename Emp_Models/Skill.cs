using Emp_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Models
{
    public class Skill : BaseEntity
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        
        public List<Employee>? Employees { get; set; }

    }
}

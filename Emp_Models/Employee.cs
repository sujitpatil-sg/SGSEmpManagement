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
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
      
        [Required]
        public string EmpFirstName { get; set; }
        [Required]
        public string EmpLastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public DateTime HiredDate { get; set; }
     
       public List<Skill>? Skills { get; set;}

    }
}

using DataAccsess.Data;
using Emp_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Repository.IRepository
{
    public class EmployeeSkillRepository : Repository<EmployeeSkill>, IEmployeeSkillRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeSkillRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(EmployeeSkill empSkill)
        {
            _context.EmpEmployeeSkills.Update(empSkill);
        }
    }
}

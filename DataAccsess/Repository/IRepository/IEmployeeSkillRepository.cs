using Emp_Models;
using Emp_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Repository.IRepository
{
    public interface IEmployeeSkillRepository:IRepository<EmployeeSkill>
    {
        void Save();
        void Update(EmployeeSkill empSkill);
    }
}

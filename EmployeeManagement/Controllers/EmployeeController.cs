using DataAccsess.Data;
using DataAccsess.Repository.IRepository;
using Emp_Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeSkillRepository _employeeSkillRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ApplicationDbContext _db;

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeSkillRepository employeeSkillRepository, ISkillRepository skillRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeSkillRepository = employeeSkillRepository;
            _skillRepository = skillRepository;
        }
        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "EmpFirstName" : "";
            ViewData["DateSortParameter"] = sortOrder == "HiredDate" ? "Date" : "Date_desc";

            ViewData["CurrentFilter"] = searchString;

            var result = _employeeRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.EmpFirstName.Contains(searchString)
                                       || s.EmpLastName.Contains(searchString));
            }
            switch (sortOrder)
            {

                case "EmpFirstName":
                    result = result.OrderByDescending(s => s.EmpFirstName);
                    break;
                case "Date_desc":
                    result = result.OrderByDescending(s => s.HiredDate);
                    break;
                case "Date":
                    result = result.OrderBy(s => s.HiredDate);
                    break;
            }

            return View(result);

        }
        [HttpGet]

        public IActionResult EmployeeDetails(int id)
        {
            var empDetails = _employeeRepository.Get(u => u.EmpId == id);
            if (empDetails != null)
            {
                empDetails.Skills = new List<Skill>();

                var empSkills = _employeeSkillRepository.GetAll().Where(x => x.EmployeesEmpId == id);
                if (empSkills != null && empSkills.Count() > 0)
                {
                    foreach (var eSkill in empSkills)
                    {
                        var skill = _skillRepository.Get(x => x.Id == eSkill.SkillsId);
                        if (skill != null)
                        {
                            empDetails.Skills.Add(skill);
                        }
                    }
                }
            }
            return View(empDetails);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            var skills = _skillRepository.GetAll();
            ViewData["Skills"] = skills.Select(s =>
            {
                return new SelectListItem()
                {
                    Text = s.SkillName,
                    Value = Convert.ToString(s.Id),
                    Selected = false
                };
            }).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployee emp)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee { Email = emp.Email, EmpFirstName = emp.EmpFirstName, EmpId = emp.EmpId, EmpLastName = emp.EmpLastName, Gender = emp.Gender, HiredDate = emp.HiredDate };


                _employeeRepository.Add(employee);
                _employeeRepository.Save();

                //Add Employee Skills

                foreach (var skillId in emp.EmpSkills)
                {
                    var empskill = new EmployeeSkill { EmployeesEmpId = employee.EmpId, SkillsId = Convert.ToInt32(skillId) };

                    _employeeSkillRepository.Add(empskill);
                }

                _employeeSkillRepository.Save();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DetailsEmployee(int id)
        {
            var employees = _employeeRepository.Get(u => u.EmpId == id);
            return View(employees);
        }
        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            var deleteSkill = _employeeRepository.Get(u => u.EmpId == id);
            return View(deleteSkill);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleteSkill = _employeeRepository.Get(u => u.EmpId == id);
            _employeeRepository.Remove(deleteSkill);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            var SkillById = _employeeRepository.Get(u => u.EmpId == id);
            return View(SkillById);
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee Emp)
        {
            _employeeRepository.Update(Emp);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
    }
}

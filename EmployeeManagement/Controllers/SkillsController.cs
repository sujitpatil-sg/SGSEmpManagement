using DataAccsess.Repository.IRepository;
using Emp_Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository iskillRepository)
        {
            _skillRepository = iskillRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _skillRepository.GetAll();
            return View(result);

        }
        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.CreatedDate = DateTime.Now;
                _skillRepository.Add(skill);
                _skillRepository.Save();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DetailsSkill(int id)
        {
            var SkillById = _skillRepository.Get(u => u.Id == id);
            return View(SkillById);
        }
        [HttpGet]
        public IActionResult DeleteSkill(int id)
        {
            var deleteSkill = _skillRepository.Get(u => u.Id == id);
            return View(deleteSkill);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleteSkill = _skillRepository.Get(u => u.Id == id);
            _skillRepository.Remove(deleteSkill);
            _skillRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var SkillById = _skillRepository.Get(u => u.Id == id);
            return View(SkillById);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            skill.LastModifiedDate = DateTime.Now;
            _skillRepository.Update(skill);
            _skillRepository.Save();
            return RedirectToAction("Index");
        }
    }
}

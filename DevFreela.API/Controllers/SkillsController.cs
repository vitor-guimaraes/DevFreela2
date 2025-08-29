using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Infrastucture.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly ISkillsService _service;

        public SkillsController(DevFreelaDbContext context, ISkillsService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var skills = _context.Skills.ToList();

            //return Ok(skills);

            var result = _service.GetAllSkills();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var skill = _context.Skills
            //    .SingleOrDefault(s => s.Id == id);

            //if (skill == null)
            //{
            //    return NotFound();
            //}

            //var model = new SkillViewModel(id, skill.Description)
            //{
            //    Id = skill.Id,
            //    Description = skill.Description
            //};
            
            //return Ok(model);

            var skill = _service.GetSkillById(id);
            return skill.IsSuccess ? Ok(skill) : NotFound(skill.Message);
        }

        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            //var skill = model.ToEntity();

            //_context.Skills.Add(skill);
            //_context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = 1 }, model);

            var skill = _service.PostSkill(model);

            return skill.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = skill.Data }, skill) 
                : BadRequest(skill.Message);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateSkillInputModel model)
        {
            //var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            //if (skill is null)
            //{
            //    return NotFound();
            //}

            //skill.Update(model.Description);

            //_context.Skills.Update(skill);
            //_context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = 1 }, model);

            var skill = _service.UpdateSkill(id, model);

            return skill.IsSuccess ? NoContent() : NotFound(skill.Message);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            //if (skill == null)
            //{
            //    return NotFound();
            //}

            //_context.Skills.Remove(skill);
            //_context.SaveChanges();

            //return NoContent();

            var skill = _service.DeleteSkill(id);

            return skill.IsSuccess ? NoContent() : NotFound(skill.Message);

        }
    }
}

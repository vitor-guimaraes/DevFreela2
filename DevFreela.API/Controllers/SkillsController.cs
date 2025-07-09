using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _context.Skills.ToList();

            return Ok(skills);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skill = _context.Skills
                .SingleOrDefault(s => s.Id == id);

            if (skill == null)
            {
                return NotFound();
            }

            var model = new SkillViewModel(id, skill.Description)
            {
                Id = skill.Id,
                Description = skill.Description
            };
            
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateSkillInputModel model)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            if (skill is null)
            {
                return NotFound();
            }

            skill.Update(model.Description);

            _context.Skills.Update(skill);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return NoContent();

        }
    }
}

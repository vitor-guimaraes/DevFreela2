using DevFreela.Application.Commands.SkillCommands.DeleteSkill;
using DevFreela.Application.Commands.SkillCommands.InsertSkill;
using DevFreela.Application.Commands.SkillCommands.UpdateSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Application.Services;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService _service;
        private readonly IMediator _mediator;


        public SkillsController(ISkillsService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var skills = _context.Skills.ToList();

            //return Ok(skills);
            var query = new GetAllSkillsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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

            //var skill = _service.GetSkillById(id);
            var query = new GetSkillByIdQuery(id);

            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillInputModel model)
        {
            //var skill = model.ToEntity();

            //_context.Skills.Add(skill);
            //_context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = 1 }, model);

            //var skill = _service.PostSkill(model);

            var command = new InsertSkillCommand(model.Description);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result) 
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateSkillInputModel model)
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

            //var skill = _service.UpdateSkill(id, model);

            var command = new UpdateSkillCommand(id, model.Description);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            //if (skill == null)
            //{
            //    return NotFound();
            //}

            //_context.Skills.Remove(skill);
            //_context.SaveChanges();

            //return NoContent();

            //var skill = _service.DeleteSkill(id);

            var command = new DeleteSkillCommand(id);
            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);

        }
    }
}

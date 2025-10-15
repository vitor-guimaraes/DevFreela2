using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Commands.UserCommands.DeleteUser;
using DevFreela.Application.Commands.UserCommands.InsertUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMediator _mediator;

        public UsersController(IUserService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var users = _context.Users
            //    .Where(u => u.Active)
            //    .ToList();
            //var model = users.Select(UserViewModel.FromEntity).ToList();
            //return Ok(model);

            //var result = await _service.GetAllUsers();

            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var user = _context.Users
            //    .Include(u => u.Skills)
            //    .ThenInclude(u => u.Skill)
            //    .SingleOrDefault(u => u.Id == id && u.Active);

            //if (user is null)
            //{
            //    return NotFound();
            //}

            //var model = UserViewModel.FromEntity(user);
            //return Ok(model);

            //var user = _service.GetUserById(id);

            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserInputModel model)
        {
            //var user = model.ToEntity();
            //_context.Users.Add(user);
            //_context.SaveChanges();
            //return CreatedAtAction(nameof(GetById), new { id = user.Id }, UserViewModel.FromEntity(user));

            //var user =_service.PostUser(model);
            //return user.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = user.Data }, user) 
            //    : BadRequest(user.Message);

            var command = new InsertUserCommand(model.Id, model.FullName, model.Email, model.Password, model.Role);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result)
                : BadRequest(result.Message);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, UserSkillsInputModel model)
        {
            //var userSkills = model.SkillIds
            //    .Select(skillId => new UserSkill(id, skillId))
            //    .ToList();

            //_context.UserSkills.AddRange(userSkills);
            //_context.SaveChanges();

            //return NoContent();

            var userSkills = _service.PostUserSkills(id, model);
            return userSkills.IsSuccess ? NoContent() : BadRequest(userSkills.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var user = _context.Users
            //    .SingleOrDefault(u => u.Id == id);

            //if (user == null)
            //{
            //    return NotFound();
            //}

            ////user.SetAsDeleted();
            //user.DeleteUser();
            //_context.Users.Update(user);
            //_context.SaveChanges();

            //return NoContent();

            //var user = _service.DeleteUser(id);
            //return user.IsSuccess ? NoContent() : NotFound(user.Message);

            var command = new DeleteUserCommand(id);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> PostProfilePicture(int id, IFormFile file)
        {
            var descritption = $"File: {file.Name}, Size: {file.Length}";

            return Ok(descritption);
        }

    }
}

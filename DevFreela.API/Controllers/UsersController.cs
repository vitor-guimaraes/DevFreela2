using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IUserService _service;
        public UsersController(DevFreelaDbContext context, IUserService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var users = _context.Users
            //    .Where(u => u.Active)
            //    .ToList();
            //var model = users.Select(UserViewModel.FromEntity).ToList();
            //return Ok(model);

            var result = _service.GetAllUsers();
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
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

            var user = _service.GetUserById(id);
            return user.IsSuccess ? Ok(user) : NotFound(user.Message);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            //var user = model.ToEntity();
            //_context.Users.Add(user);
            //_context.SaveChanges();
            //return CreatedAtAction(nameof(GetById), new { id = user.Id }, UserViewModel.FromEntity(user));

            var user =_service.PostUser(model);
            return user.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = user.Data }, user) 
                : BadRequest(user.Message);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
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
        public IActionResult Delete(int id)
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

            var user = _service.DeleteUser(id);
            return user.IsSuccess ? NoContent() : NotFound(user.Message);
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var descritption = $"File: {file.Name}, Size: {file.Length}";

            return Ok(descritption);
        }

    }
}

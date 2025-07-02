using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(UserSkillsInputModel model)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file)
        {
            var descritption = $"File: {file.Name}, Size: {file.Length}";

            return Ok(descritption);
        }

    }
}

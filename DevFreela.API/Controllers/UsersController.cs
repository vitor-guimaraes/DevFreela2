using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
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

using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _service;
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll(string search = "")
        {
            var result = _service.GetAllProjects();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetProjectById(id);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        public IActionResult PostProject(CreateProjectInputModel model)
        {
            var result = _service.PostProject(model);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result) 
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, UpdateProjectInputModel model) 
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //result.Update(model.Title, model.Description, model.TotalCost);

            //_context.Projects.Update(result);
            //_context.SaveChanges();

            var result =_service.UpdateProject(id, model);

            //return id == model.IdProject ? NoContent() : BadRequest();
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //result.SetAsDeleted();

            //_context.Projects.Update(result);
            //_context.SaveChanges();

            //return NoContent();

            var result = _service.DeleteProject(id);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteProject(int id)
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //result.Complete();

            //_context.Projects.Update(result);
            //_context.SaveChanges();

            //return NoContent();

            var result = _service.CompleteProject(id);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("{id}/start")]
        public IActionResult StartProject(int id)
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //result.Start();

            //_context.Projects.Update(result);
            //_context.SaveChanges();

            //return NoContent();
            var result = _service.StartProject(id);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //var comment = new ProjectComment(model.Content, id, model.IdUser);

            //_context.ProjectComments.Add(comment);
            //_context.SaveChanges();

            //return NoContent();
            var result = _service.PostComment(id, model);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }


    }
}

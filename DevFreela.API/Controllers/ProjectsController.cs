using DevFreela.Application.Commands.ProjectCommands.CompleteProject;
using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Application.Commands.ProjectCommands.InsertComment;
using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Commands.ProjectCommands.StartProject;
using DevFreela.Application.Commands.ProjectCommands.UpdateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _service;
        private readonly IMediator _mediator;
        public ProjectsController(IProjectService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "")
        {
            //var result = _service.GetAllProjects();
            var query = new GetAllProjectsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var result = _service.GetProjectById(id);

            var query = new GetProjectByIdQuery(id);

            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject(CreateProjectInputModel model)
        {
            //var result = _service.PostProject(model);

            var command = new InsertProjectCommand(model.Title, model.Description, model.IdClient, model.IdFreelancer, model.TotalCost);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result) 
                : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectInputModel model) 
        {
            //var result = _context.Projects.SingleOrDefault(p => p.Id == id);

            //if (result is null)
            //{
            //    return BadRequest();
            //}

            //result.Update(model.Title, model.Description, model.TotalCost);

            //_context.Projects.Update(result);
            //_context.SaveChanges();

            //return id == model.IdProject ? NoContent() : BadRequest();

            //var result =_service.UpdateProject(id, model);

            var command = new UpdateProjectCommand(id, model.Title, model.Description, model.TotalCost);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
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

            //var result = _service.DeleteProject(id);

            var command = new DeleteProjectCommand(id);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteProject(int id)
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

            //var result = _service.CompleteProject(id);

            var command = new CompleteProjectCommand(id);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartProject(int id)
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
            //var result = _service.StartProject(id);

            var command = new StartProjectCommand(id);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, CreateProjectCommentInputModel model)
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
            //var result = _service.PostComment(id, model);

            var command = new InsertCommentCommand(id, model.IdUser, model.Content);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }


    }
}

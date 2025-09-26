using DevFreela.Application.Commands.ProjectCommands.CompleteProject;
using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Project not found");
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Project deleted");
        }
    }
}

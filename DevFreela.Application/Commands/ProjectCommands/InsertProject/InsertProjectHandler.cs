using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public InsertProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            // Validate that the client exists
            var clientExists = _context.Users.Any(u => u.Id == request.IdClient);
            if (!clientExists)
            {
                return ResultViewModel<int>.Error("Client user not found.");
            }

            // Validate that the freelancer exists
            var freelancerExists = _context.Users.Any(u => u.Id == request.IdFreelancer);
            if (!freelancerExists)
            {
                return ResultViewModel<int>.Error("Freelancer user not found.");
            }

            var project = request.ToEntity();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}

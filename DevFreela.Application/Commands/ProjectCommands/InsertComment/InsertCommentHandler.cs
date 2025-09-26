using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    internal class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertCommentHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == request.IdProject);

            if (project is null)
            {
                return new ResultViewModel(false, "Project not found");
            }

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}

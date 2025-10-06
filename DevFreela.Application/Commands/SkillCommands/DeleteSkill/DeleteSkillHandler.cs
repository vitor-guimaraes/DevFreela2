using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.SkillCommands.DeleteSkill
{
    internal class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Id == request.Id);

            if (skill == null)
            {
                return ResultViewModel.Error("Skill not found");
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}

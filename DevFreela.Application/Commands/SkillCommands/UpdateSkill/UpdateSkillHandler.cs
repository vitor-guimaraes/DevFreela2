using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.SkillCommands.UpdateSkill
{
    internal class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public UpdateSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Id == request.Id);

            if (skill is null)
            {
                return ResultViewModel.Error("Skill not found");
            }

            skill.Update(request.Description);

            _context.Skills.Update(skill);
            _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}

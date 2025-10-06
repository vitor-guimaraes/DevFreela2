using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllSkillsHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills.ToListAsync();

            if (skills == null || skills.Count == 0)
            {
                return ResultViewModel<List<SkillViewModel>>.Error("No skills found");
            }

            var model = skills.ToList()
                        .Select(s => new SkillViewModel(s.Id, s.Description))
                        .ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}

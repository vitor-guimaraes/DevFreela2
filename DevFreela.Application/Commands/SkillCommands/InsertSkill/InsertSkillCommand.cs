using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel<int>>
    {
        public InsertSkillCommand(string description)
        {
            Description = description;
        }
        public string Description { get; set; }

        public Skill ToEntity()
            => new(Description);
    }
}

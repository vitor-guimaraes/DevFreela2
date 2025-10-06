using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest<ResultViewModel>
    {
        public UpdateSkillCommand(int id, string description)
        {
            Id = id;
            Description = description;
        }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}

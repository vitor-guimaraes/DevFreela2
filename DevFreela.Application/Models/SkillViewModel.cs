using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillViewModel
    {
        public SkillViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public static SkillViewModel FromEntity(Skill entity)
        {
            return new SkillViewModel(entity.Id, entity.Description);
        }
    }
}
using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;

namespace DevFreela.Application.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly DevFreelaDbContext _context;
        public SkillsService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel DeleteSkill(int id)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            if (skill == null)
            {
                return ResultViewModel.Error("Skill not found");
            }

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<SkillViewModel>> GetAllSkills()
        {

            var skills = _context.Skills.ToList();

            if (skills == null || skills.Count == 0)
            {
                return ResultViewModel<List<SkillViewModel>>.Error("No skills found");
            }

            var model = skills.ToList()
                        .Select(s => new SkillViewModel(s.Id, s.Description))
                        .ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);

        }

        public ResultViewModel<SkillViewModel> GetSkillById(int id)
        {
           var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            if (skill == null)
            {
                return ResultViewModel<SkillViewModel>.Error("Skill not found");
            }

            var model = new SkillViewModel(skill.Id, skill.Description);
            return ResultViewModel<SkillViewModel>.Success(model);
        }

        public ResultViewModel<int> PostSkill(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(skill.Id);
        }
        public ResultViewModel UpdateSkill(int id, UpdateSkillInputModel model)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == id);

            if (skill is null)
            {
                return ResultViewModel.Error("Skill not found");
            }

            skill.Update(model.Description);

            _context.Skills.Update(skill);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}

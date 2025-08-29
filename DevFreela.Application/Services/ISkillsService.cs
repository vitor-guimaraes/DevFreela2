using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface ISkillsService
    {
        ResultViewModel<List<SkillViewModel>> GetAllSkills();
        ResultViewModel<SkillViewModel> GetSkillById(int id);
        ResultViewModel<int> PostSkill(CreateSkillInputModel model);
        ResultViewModel UpdateSkill(int id, UpdateSkillInputModel model);
        ResultViewModel DeleteSkill(int id);

    }
}

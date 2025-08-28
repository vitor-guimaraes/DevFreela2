using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        ResultViewModel <List<UserViewModel>> GetAllUsers();
        ResultViewModel<UserViewModel> GetUserById(int id);
        ResultViewModel<int> PostUser(CreateUserInputModel model);
        //ResultViewModel UpdateUser(int id, UpdateUserInputModel model);
        ResultViewModel DeleteUser(int id);
        ResultViewModel PostUserSkills(int id, UserSkillsInputModel model);

    }
}

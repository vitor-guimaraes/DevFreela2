using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel DeleteUser(int id)
        {
            if (id == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }
            
            var user = _context.Users
                        .SingleOrDefault(u => u.Id == id && u.Active);

            if (user != null)
            {
                user.DeleteUser();
                _context.SaveChanges();
                return ResultViewModel.Success();
            }
            else
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }
        }

        public ResultViewModel<List<UserViewModel>> GetAllUsers()
        {
            var users = _context.Users
                        .Where(u => u.Active)
                        .ToList();
            
            var model = users.Select(UserViewModel.FromEntity).ToList();
            
            return ResultViewModel<List<UserViewModel>>.Success(model);
        }

        public ResultViewModel<UserViewModel> GetUserById(int id)
        {
            if (id == null) 
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }
            
            var user = _context.Users 
                        .SingleOrDefault(u => u.Id == id && u.Active);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }
            return ResultViewModel<UserViewModel>.Success(UserViewModel.FromEntity(user));

        }

        public ResultViewModel<int> PostUser(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();
         
            return ResultViewModel<int>.Success(user.Id);
        }

        public ResultViewModel PostUserSkills(int id, UserSkillsInputModel model)
        {
            var user = _context.Users.
                SingleOrDefault(u => u.Id == id && u.Active);

            if (user is null)
            {
                return ResultViewModel.Error("User not found");
            }

            var userSkills = model.SkillIds
                .Select(skillId => new UserSkill(id, skillId))
                .ToList();

            if (userSkills == null || userSkills.Count == 0)
                return ResultViewModel<UserViewModel>.Error("User not found");

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        //public ResultViewModel UpdateUser(int id, UpdateUserInputModel model)
        //{
        //    var user = _context.Users
        //                .SingleOrDefault(u => u.Id == id && u.Active);

        //    if (user is null)
        //    {
        //        return ResultViewModel.Error("User not found");
        //    }

        //    user.UpdateUser(model.FullName, model.Email, model.BirthDate);

        //    _context.Users.Update(user);
        //    _context.SaveChanges();

        //    return ResultViewModel.Success();
        //}
    }
}

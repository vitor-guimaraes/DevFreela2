using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;
        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel CompleteProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Project not found");
            }

            project.Complete();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel DeleteProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Project not found");
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return new ResultViewModel(true, "Project deleted");
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAllProjects(string search = "")
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectItemViewModel> GetProjectById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel<ProjectItemViewModel>.Error("Project not found");
            }

            var model = ProjectItemViewModel.FromEntity(project);

            return ResultViewModel<ProjectItemViewModel>.Success(model);
        }

        public ResultViewModel PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return new ResultViewModel(false, "Project not found");
            }

            var comment = new ProjectComment(model.Content, id, model.IdUser);

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<int> PostProject(CreateProjectInputModel model)
        {
            // Validate that the client exists
            var clientExists = _context.Users.Any(u => u.Id == model.IdClient);
            if (!clientExists)
            {
                return ResultViewModel<int>.Error("Client user not found.");
            }

            // Validate that the freelancer exists
            var freelancerExists = _context.Users.Any(u => u.Id == model.IdFreelancer);
            if (!freelancerExists)
            {
                return ResultViewModel<int>.Error("Freelancer user not found.");
            }

            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(project.Id);
        }

        public ResultViewModel StartProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Project not found");
            }

            project.Start();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();

        }

        public ResultViewModel UpdateProject(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == model.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error("Project not found");
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();

        }
    }
}

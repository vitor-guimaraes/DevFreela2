using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAllProjects(string search = "");
        ResultViewModel<ProjectItemViewModel> GetProjectById(int id);
        ResultViewModel<int> PostProject(CreateProjectInputModel model);
        ResultViewModel UpdateProject(int id, UpdateProjectInputModel model);
        ResultViewModel DeleteProject(int id);
        ResultViewModel CompleteProject(int id);
        ResultViewModel StartProject(int id);
        ResultViewModel PostComment(int id, CreateProjectCommentInputModel model);
    }
}

using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.UpdateUserPicture
{

    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }

        public UpdateUserCommand(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

    }
}

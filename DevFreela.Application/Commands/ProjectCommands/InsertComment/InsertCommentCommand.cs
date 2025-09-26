using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {
        public InsertCommentCommand(int idProject, int idUser, string content)
        {
            IdProject = idProject;
            IdUser = idUser;
            Content = content;
        }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
        public string Content { get; set; }
    }
}

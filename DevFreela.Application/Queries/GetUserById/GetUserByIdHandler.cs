using DevFreela.Application.Models;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly DevFreelaDbContext _context;
        public GetUserByIdHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }

            var user = await _context.Users
                        .SingleOrDefaultAsync(u => u.Id == request.Id && u.Active);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found");
            }
            return ResultViewModel<UserViewModel>.Success(UserViewModel.FromEntity(user));
        }
    }
}

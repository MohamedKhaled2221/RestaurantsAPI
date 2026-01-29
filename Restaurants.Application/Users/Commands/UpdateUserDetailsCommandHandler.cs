using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
       IUserContext userContext,IUserStore<User> userstore ) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user : {UserId}, with {@Request}",user!.Id, request);

            var dbUser =await userstore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
                throw new NotFoundException(nameof(User), user!.Id);

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DataofBirth;

            await userstore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}

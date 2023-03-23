using EVN.Core.Exceptions;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.UserCommand
{
    public class ForgotPassword : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class ForgotPasswordHandler : IRequestHandler<ForgotPassword, bool>
    {
        private readonly UserManager<User> _userManager;
        public ForgotPasswordHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(ForgotPassword request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new EvnException();
            }
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, "EVNHaNoi@12345");
            return true;
        }
    }
}

using Authentication.Application.Model.Auth;
using MediatR;

namespace Authentication.Application.Commands.AuthCommand
{
    /// <summary>
    /// LoginCommand
    /// </summary>
    public class LoginCommand : IRequest<LoginResponse>
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.Auth
{
    public class LoginResponse
    {
        public LoginResponse(string accessToken, int expiresIn, string refreshToken, Guid userId, string userName, string fullName, string phoneNumber, string email, string permissions, bool isAdministrator, string position)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
            UserId = userId;
            UserName = userName;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Permissions = permissions ?? "";
            IsAdministrator = isAdministrator;
            Position = position;
        }

        /// <summary>
        /// Access Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Time expired Access Token
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        public string Permissions { get; set; }
        public bool IsAdministrator { get; set; }
        public string Position{ get; set; }

    }
}

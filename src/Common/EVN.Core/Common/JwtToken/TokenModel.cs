namespace EVN.Core.Common.JwtToken
{
    /// <summary>
    /// Token Model
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// TokenModel
        /// </summary>
        public TokenModel() { }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// IsSuperAdmin
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// FullName
        /// </summary>
        public string Name { get; set; }

    }
}

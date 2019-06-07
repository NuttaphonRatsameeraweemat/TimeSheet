using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TimeSheet.Bll.Interfaces
{
    public interface IRefreshTokenBll
    {
        /// <summary>
        /// Validate Token Parameters annd SecurityToken from token parameter.
        /// </summary>
        /// <param name="token">The token expires value.</param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        /// <summary>
        /// Generate new refresh token and save.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <returns></returns>
        string GenerateRefreshToken(string email);
        /// <summary>
        /// Validate refresh token in storage.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <param name="refreshToken">The refresh token value.</param>
        /// <returns></returns>
        bool ValidateRefreshToken(string email, string refreshToken);
    }
}

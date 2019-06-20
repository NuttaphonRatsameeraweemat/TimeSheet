using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Helper;

namespace TimeSheet.Bll
{
    public class RefreshTokenBll : IRefreshTokenBll
    {

        #region [Fields]

        /// <summary>
        /// The config value in appsetting.json
        /// </summary>
        private readonly IConfiguration _config;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenBll" /> class.
        /// </summary>
        /// <param name="config">The config value.</param>
        public RefreshTokenBll(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Validate Token Parameters annd SecurityToken from token parameter.
        /// </summary>
        /// <param name="token">The token expires value.</param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                principal = null;
            }

            return principal;
        }

        /// <summary>
        /// Generate new refresh token and save.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <returns></returns>
        public string GenerateRefreshToken(string email)
        {
            string token;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                token = string.Join("", tokenData.Select(b => b.ToString("x2")).ToArray());
            }
            this.SaveRefreshToken(email, token);
            return token;
        }

        /// <summary>
        /// Validate refresh token in storage.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <param name="refreshToken">The refresh token value.</param>
        /// <returns></returns>
        public bool ValidateRefreshToken(string email, string refreshToken)
        {
            var userInfo = RedisCacheHandler.GetValue<RefreshTokenModel>(email);
            if (userInfo != null && userInfo.RefreshToken == refreshToken)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get Refresh Token.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <returns></returns>
        public string GetRefreshToken(string email)
        {
            string result = string.Empty;
            var userInfo = RedisCacheHandler.GetValue<RefreshTokenModel>(email);
            if (userInfo != null)
            {
                result = userInfo.RefreshToken;
            }
            return result; 
        }

        /// <summary>
        /// Save refresh token into storage.
        /// </summary>
        /// <param name="email">The owner refresh token.</param>
        /// <param name="refreshToken">The refresh token value.</param>
        private void SaveRefreshToken(string email, string refreshToken)
        {
            var model = new RefreshTokenModel { RefreshToken = refreshToken };
            RedisCacheHandler.SetValue(email, model);
        }

        #endregion

    }
}

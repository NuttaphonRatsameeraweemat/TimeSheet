﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeSheet.Data.Pocos;
using TimeSheet.Data.Repository.Interfaces;
using System.Linq;
using TimeSheet.Bll.Components;

namespace TimeSheet.Bll
{
    public class LoginBll : ILoginBll
    {

        #region [Fields]

        /// <summary>
        /// The config value in appsetting.json
        /// </summary>
        private readonly IConfiguration _config;
        /// <summary>
        /// The utilities unit of work for manipulating utilities data in database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The ClaimsIdentity.
        /// </summary>
        private ClaimsIdentity _identity;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginBll" /> class.
        /// </summary>
        /// <param name="config">The config value.</param>
        public LoginBll(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Validate username and password is valid.
        /// </summary>
        /// <param name="login">The login value.</param>
        /// <returns></returns>
        public bool Authenticate(LoginViewModel login, EmployeeViewModel model)
        {
            bool result = false;
            var data = _unitOfWork.GetRepository<Employee>().Get(x => x.Email == login.Username).FirstOrDefault();
            if (data != null && ValidatePassword(login))
            {
                result = true;
                this.ManageClaimsIdentity(data, model);
                this.DeclareEmployeeInformation(model, data);
            }
            return result;
        }

        /// <summary>
        /// Declare Employee Information for return.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="data"></param>
        private void DeclareEmployeeInformation(EmployeeViewModel model, Employee data)
        {
            model.Name = string.Format(ConstantValue.EMP_TEMPLATE, data.FirstName, data.LastName);
            model.StartWorkingDay = data.StartWorkingDay.Value.ToString(ConstantValue.DATE_TIME_FORMAT);
            model.Email = data.Email;
            model.TelNo = data.TelNo;
        }

        /// <summary>
        /// The Method Add ClaimsIdentity Properties.
        /// </summary>
        /// <param name="data">The employee information.</param>
        private void ManageClaimsIdentity(Employee data, EmployeeViewModel model)
        {
            StringBuilder roles = new StringBuilder();
            var userRoles = _unitOfWork.GetRepository<UserRole>().Get(x => x.Email == data.Email);
            var roleList = _unitOfWork.GetRepository<Role>().Get().ToList();
            _identity = new ClaimsIdentity();
            _identity.AddClaim(new Claim(ClaimTypes.Name, data.Email));
            _identity.AddClaim(new Claim(ConstantValue.CLAMIS_NAME, string.Format(ConstantValue.EMP_TEMPLATE, data.FirstName, data.LastName)));
            foreach (var userRole in userRoles)
            {
                var role = roleList.FirstOrDefault(x => x.RoleId == userRole.RoleId);
                string roleName = role.RoleName ?? "Unknow";
                _identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                roles.Append($"{roleName} ");
            }
            model.Role = roles.ToString();
        }

        /// <summary>
        /// The Verify Password.
        /// </summary>
        /// <param name="login">The login value.</param>
        /// <returns></returns>
        private bool ValidatePassword(LoginViewModel login)
        {
            var password = _unitOfWork.GetRepository<Password>().Get(x => x.Email == login.Username).FirstOrDefault();
            var verifyPassword = new PasswordGenerator(password != null ? password.Password1 : new byte[64]);
            return verifyPassword.Verify(login.Password);
        }

        /// <summary>
        /// Create and setting payload on token.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string BuildToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: _identity.Claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

    }
}

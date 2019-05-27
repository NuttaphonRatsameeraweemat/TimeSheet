using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheet.Bll.Components;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.Bll
{
    public class EmployeeBll : IEmployeeBll
    {

        #region [Fields]

        /// <summary>
        /// The utilities unit of work for manipulating utilities data in database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public EmployeeBll(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get All Employye Information.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var result = new List<EmployeeViewModel>();
            var dataEmp = _unitOfWork.GetRepository<Data.Pocos.Employee>().Get().ToList();
            var dataRoles = _unitOfWork.GetRepository<Data.Pocos.Role>().Get().ToList();

            foreach (var item in dataEmp)
            {
                result.Add(this.InitialModel(item, GetRoles(item.Email, dataRoles)));
            }

            return result;
        }

        /// <summary>
        /// Get Roles by employee email.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <param name="dataRoles">The roles information data.</param>
        /// <returns></returns>
        private string GetRoles(string email, IEnumerable<Data.Pocos.Role> dataRoles)
        {
            string result = string.Empty;
            var userRoles = _unitOfWork.GetRepository<Data.Pocos.UserRole>().Get(x => x.Email == email).ToList();
            foreach (var item in userRoles)
            {
                var data = dataRoles.FirstOrDefault(x => x.RoleId == item.RoleId);
                if (data != null)
                {
                    result += $"{data.RoleName} ";
                }
            }
            return result;
        }

        /// <summary>
        /// Mapping Pocos Db Model to View Model.
        /// </summary>
        /// <param name="data">The employee information</param>
        /// <param name="roles">The disaply roles.</param>
        /// <returns></returns>
        private EmployeeViewModel InitialModel(Data.Pocos.Employee data, string roles)
        {
            return new EmployeeViewModel
            {
                Email = data.Email,
                Name = string.Format(ConstantValue.EMP_TEMPLATE, data.FirstName, data.LastName),
                StartWorkingDay = data.StartWorkingDay.Value.ToString(ConstantValue.DATE_TIME_FORMAT),
                TelNo = data.TelNo,
                Role = roles.Trim()
            };
        }

        #endregion

    }
}

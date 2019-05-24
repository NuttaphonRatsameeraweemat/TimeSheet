using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Repository.Interfaces;
using TimeSheet.Data.Pocos;
using System.Transactions;
using System.Linq;

namespace TimeSheet.Bll
{
    public class RegisterBll : IRegisterBll
    {

        #region [Fields]

        /// <summary>
        /// The utilities unit of work for manipulating utilities data in database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The auto mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public RegisterBll(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// The Register new employee function.
        /// </summary>
        /// <param name="formData">The employee data.</param>
        /// <returns></returns>
        public ResultViewModel Register(RegisterViewModel formData)
        {
            var result = ValidateEmail(formData.Email);
            if (!result.IsError)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    this.SaveEmployee(formData);
                    this.SavePassword(formData);
                    scope.Complete();
                }
            }
            return result;
        }

        /// <summary>
        /// The Method insert employee information.
        /// </summary>
        /// <param name="formData">The employee data.</param>
        private void SaveEmployee(RegisterViewModel formData)
        {
            var data = _mapper.Map<RegisterViewModel, Employee>(formData);
            _unitOfWork.GetRepository<Employee>().Add(data);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// The Method insert password employee login.
        /// </summary>
        /// <param name="formData">The employee data.</param>
        private void SavePassword(RegisterViewModel formData)
        {
            var password = new TimeSheet.Bll.Components.PasswordGenerator(formData.Password);
            var data = new Password { Email = formData.Email, Password1 = password.GetHash() };
            _unitOfWork.GetRepository<Password>().Add(data);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Validate Email is already have in database or not.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <returns></returns>
        public ResultViewModel ValidateEmail(string email)
        {
            var result = new ResultViewModel();
            var data = _unitOfWork.GetRepository<Employee>().Get(x => x.Email == email).FirstOrDefault();
            if (data != null)
            {
                result.IsError = true;
                result.Message = "This email is already.";
            }
            return result;
        }

        #endregion

    }
}

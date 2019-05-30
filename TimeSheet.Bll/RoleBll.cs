using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheet.Bll.Components;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Pocos;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.Bll
{
    public class RoleBll : IRoleBll
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
        /// Initializes a new instance of the <see cref="RoleBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public RoleBll(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Insert new role to table role.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        public ResultViewModel Save(RoleViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Role>().Add(_mapper.Map<RoleViewModel, Role>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Update exits role information.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        public ResultViewModel Update(RoleViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Role>().Update(_mapper.Map<RoleViewModel, Role>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Delete role to table.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        public ResultViewModel Delete(RoleViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Role>().Remove(_mapper.Map<RoleViewModel, Role>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Get Role Detail and Information. 
        /// </summary>
        /// <param name="id">The Identity role.</param>
        /// <returns></returns>
        public RoleViewModel Get(int id)
        {
            return _mapper.Map<Role, RoleViewModel>(
                _unitOfWork.GetRepository<Role>().Get(x => x.RoleId == id).FirstOrDefault());
        }

        /// <summary>
        /// Get Role List.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleViewModel> GetList()
        {
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(
                _unitOfWork.GetRepository<Role>().Get());
        }

        #endregion

    }
}

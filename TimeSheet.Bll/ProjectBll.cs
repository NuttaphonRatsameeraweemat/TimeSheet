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
    public class ProjectBll : IProjectBll
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
        /// Initializes a new instance of the <see cref="ProjectBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public ProjectBll(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Insert new project to table project.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        public ResultViewModel Save(ProjectViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Project>().Add(_mapper.Map<ProjectViewModel, Project>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Update exits project information.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        public ResultViewModel Update(ProjectViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Project>().Update(_mapper.Map<ProjectViewModel, Project>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Delete project to table.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        public ResultViewModel Delete(ProjectViewModel formData)
        {
            var result = new ResultViewModel();
            _unitOfWork.GetRepository<Project>().Remove(_mapper.Map<ProjectViewModel, Project>(formData));
            _unitOfWork.Complete();
            return result;
        }

        /// <summary>
        /// Get Project Detail and Information. 
        /// </summary>
        /// <param name="id">The Identity project.</param>
        /// <returns></returns>
        public ProjectViewModel Get(int id)
        {
            return _mapper.Map<Project, ProjectViewModel>(
                _unitOfWork.GetRepository<Project>().Get(x => x.Id == id).FirstOrDefault());
        }

        /// <summary>
        /// Get Project List.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProjectViewModel> GetList()
        {
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(
                _unitOfWork.GetRepository<Project>().Get());
        }

        /// <summary>
        /// Get Project List Status Active only.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProjectViewModel> GetListActive()
        {
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(
                _unitOfWork.GetRepository<Project>().Get(x=>x.Status == ConstantValue.PROJECT_STATUS_ACTIVE));
        }

        #endregion


    }
}

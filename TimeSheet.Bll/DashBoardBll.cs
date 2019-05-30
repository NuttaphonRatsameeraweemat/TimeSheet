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
    public class DashBoardBll : IDashBoardBll
    {

        #region [Fields]

        /// <summary>
        /// The utilities unit of work for manipulating utilities data in database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValueHelpBll _valueHelp;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="DashBoardBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public DashBoardBll(IUnitOfWork unitOfWork, IValueHelpBll valueHelp)
        {
            _unitOfWork = unitOfWork;
            _valueHelp = valueHelp;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get Summary stat of project type total working.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <returns></returns>
        public List<DashBoardViewModel.ProjectTypeWorkingStat> GetProjectTypeStat(string email)
        {
            var result = new List<DashBoardViewModel.ProjectTypeWorkingStat>();

            var timeSheet = _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Get(x => x.Email == email);
            var taskList = _unitOfWork.GetRepository<TaskList>().Get(x => timeSheet.Any(y => y.Id == x.TimeSheetId));

            var projectList = _unitOfWork.GetRepository<Project>().Get();
            var projectTypeList = _valueHelp.Get(ConstantValue.VALUE_PROJECT_TYPE);

            foreach (var item in projectList)
            {
                result.Add(new DashBoardViewModel.ProjectTypeWorkingStat
                {
                    ProjectName = item.ProjectName,
                    TypeStat = this.GetStatProjectType(projectTypeList, taskList.Where(x => x.ProjectCode == item.ProjectCode))
                });
            }

            return result;
        }

        /// <summary>
        /// Get data stat project type.
        /// </summary>
        /// <param name="projectTypeList">The List of all project type.</param>
        /// <param name="taskList">The tasklist information.</param>
        /// <returns></returns>
        private List<DashBoardViewModel.TypeStat> GetStatProjectType(IEnumerable<ValueHelpViewModel> projectTypeList, IEnumerable<TaskList> taskList)
        {
            var result = new List<DashBoardViewModel.TypeStat>();
            foreach (var item in projectTypeList)
            {
                var tasks = taskList.Where(x => x.TypeCode == item.ValueKey).ToList();
                result.Add(new DashBoardViewModel.TypeStat { TypeName = item.ValueText, TotalWorking = tasks.Sum(x => x.WorkingHours.Value) });
            }
            return result;
        }

        #endregion

    }
}

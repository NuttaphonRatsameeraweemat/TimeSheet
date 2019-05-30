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
        /// <summary>
        /// The Login manager provides Login functionality.
        /// </summary>
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
                var tasks = taskList.Where(x => x.ProjectCode == item.ProjectCode).ToList();
                if (tasks.Count > 0)
                {
                    result.Add(new DashBoardViewModel.ProjectTypeWorkingStat
                    {
                        ProjectName = item.ProjectName,
                        TypeStat = this.GetStatProjectType(projectTypeList, tasks)
                    });
                }
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

        /// <summary>
        /// Get Summary project stat overview.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <param name="year">The year target.</param>
        /// <returns></returns>
        public List<DashBoardViewModel.ProjectTypeWorkingStat> GetProjectStat(string email, string year)
        {
            var result = new List<DashBoardViewModel.ProjectTypeWorkingStat>();

            var timeSheet = _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Get(x => x.Email == email &&
                                                                                       x.DateTimeStamp.Value.Date >= this.GetStartYear(year).Date &&
                                                                                       x.DateTimeStamp.Value.Date <= this.GetEndYear(year).Date);
            var taskList = _unitOfWork.GetRepository<TaskList>().Get(x => timeSheet.Any(y => y.Id == x.TimeSheetId));

            var projectList = _unitOfWork.GetRepository<Project>().Get();

            var gruopTask = taskList.Select(x => x.ProjectCode).Distinct();
            projectList = projectList.Where(x => gruopTask.Contains(x.ProjectCode));

            foreach (var item in projectList)
            {
                result.Add(new DashBoardViewModel.ProjectTypeWorkingStat
                {
                    ProjectName = item.ProjectName,
                    TypeStat = this.GetSummaryYear(taskList, timeSheet)
                });
            }

            return result;
        }

        /// <summary>
        /// Get Project Stat in year.
        /// </summary>
        /// <param name="taskList">The task list information.</param>
        /// <param name="timeSheets">the time sheet information.</param>
        /// <returns></returns>
        private List<DashBoardViewModel.TypeStat> GetSummaryYear(IEnumerable<TaskList> taskList, IEnumerable<Data.Pocos.TimeSheet> timeSheets)
        {
            var result = new List<DashBoardViewModel.TypeStat>();
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            for (int i = 0; i < months.Length; i++)
            {
                var timeSheetId = timeSheets.Where(x => x.DateTimeStamp.Value.Month == (i + 1)).Select(x => x.Id);
                var totalWorking = taskList.Where(x => timeSheetId.Contains(x.TimeSheetId.Value)).Sum(x => x.WorkingHours.Value);
                result.Add(new DashBoardViewModel.TypeStat
                {
                    TypeName = months[i],
                    TotalWorking = totalWorking
                });
            }
            return result;
        }

        /// <summary>
        /// Get DateTime start target year.
        /// </summary>
        /// <param name="year">The target year.</param>
        /// <returns></returns>
        private DateTime GetStartYear(string year)
        {
            return new DateTime(Convert.ToInt32(year), 1, 1);
        }

        /// <summary>
        /// Get DateTime end target year.
        /// </summary>
        /// <param name="year">The target year.</param>
        /// <returns></returns>
        private DateTime GetEndYear(string year)
        {
            return new DateTime(Convert.ToInt32(year), 12, 31);
        }

        #endregion

    }
}

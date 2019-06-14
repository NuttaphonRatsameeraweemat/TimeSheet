using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using TimeSheet.Bll.Components;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Repository.Interfaces;
using TimeSheet.Helper;
using TimeSheet.Helper.Models;

namespace TimeSheet.Bll
{
    public class TimeSheetBll : ITimeSheetBll
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
        /// Initializes a new instance of the <see cref="TimeSheetBll" /> class.
        /// </summary>
        /// <param name="unitOfWork">The utilities unit of work.</param>
        public TimeSheetBll(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get TimeSheet information by employee email.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <param name="date">The target month.</param>
        /// <returns></returns>
        public TimeSheetViewModel Get(string email, string date)
        {
            var result = new TimeSheetViewModel();
            result.TimeSheet.AddRange(GetTimeSheet(email, date));
            return result;
        }

        /// <summary>
        /// Get TimeSheet Stamp.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <param name="date">The target month.</param>
        /// <returns></returns>
        private IEnumerable<TimeSheetModel> GetTimeSheet(string email, string date)
        {
            var result = new List<TimeSheetModel>();
            var startDate = SetToFirstDay(ConvertToDateTime(date));
            var endDate = SetToLastDay(startDate);

            var dataList = _unitOfWork.GetRepository<TimeSheet.Data.Pocos.TimeSheet>().Get(x => x.Email == email &&
                                                                                           (x.DateTimeStamp.Value.Date >= startDate.Date &&
                                                                                           x.DateTimeStamp.Value.Date <= endDate.Date));

            foreach (var data in dataList)
            {
                result.Add(new TimeSheetModel
                {
                    TimeSheetId = data.Id,
                    DateTimeStamp = data.DateTimeStamp.Value.ToString("yyyy-MM-dd"),
                    TaskList = this.GetTaskList(data.Id).ToList()
                });
            }

            return result;
        }

        /// <summary>
        /// Get Task list timesheet.
        /// </summary>
        /// <param name="timeSheetId">The timesheet id.</param>
        /// <returns></returns>
        private IEnumerable<TaskListModel> GetTaskList(int timeSheetId)
        {
            return _mapper.Map<IEnumerable<TimeSheet.Data.Pocos.TaskList>, IEnumerable<TaskListModel>>(
                _unitOfWork.GetRepository<TimeSheet.Data.Pocos.TaskList>().Get(x => x.TimeSheetId == timeSheetId));
        }

        /// <summary>
        /// The Method set date to first day of month.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DateTime SetToFirstDay(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// The Method set date to last day of month.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DateTime SetToLastDay(DateTime value)
        {
            return value.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Convert string to date time using yyyy-MM-dd format.
        /// </summary>
        /// <param name="value">The string datetime.</param>
        /// <returns></returns>
        private DateTime ConvertToDateTime(string value)
        {
            return DateTime.TryParseExact(value, ConstantValue.DATE_TIME_FORMAT,
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, out DateTime temp) ? temp : throw new ArgumentException($"DateTime incorrect format : {value}");
        }

        /// <summary>
        /// Insert new timesheet and tasklist information to database.
        /// </summary>
        /// <param name="formData">The information of timesheet and tasklist.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        /// <returns></returns>
        public ResultViewModel Save(TimeSheetViewModel formData, string email)
        {
            var result = new ResultViewModel();
            using (TransactionScope scope = new TransactionScope())
            {
                this.SaveTimeSheet(formData.TimeSheet, email, ref result);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// Insert TimeSheet to database.
        /// </summary>
        /// <param name="timeSheetList">The TimeSheet information.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        private void SaveTimeSheet(List<TimeSheetModel> timeSheetList, string email, ref ResultViewModel result)
        {
            foreach (var item in timeSheetList)
            {
                var data = new TimeSheet.Data.Pocos.TimeSheet
                {
                    Email = email,
                    DateTimeStamp = ConvertToDateTime(item.DateTimeStamp)
                };

                if (!IsAlreadyDateTimeStamp(data.DateTimeStamp.Value))
                {
                    _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Add(data);
                    _unitOfWork.Complete();

                    item.TaskList = item.TaskList.Select(c => { c.TimeSheetId = data.Id; return c; }).ToList();
                    SaveTaskList(item.TaskList);
                }
                else result = UtilityService.InitialResultError($"The {data.DateTimeStamp.Value.ToString("yyyy-MM-dd")} is already exits.");
            }
        }

        /// <summary>
        /// Validate Exits Datetime stamp in storage.
        /// </summary>
        /// <param name="date">The target date.</param>
        /// <returns></returns>
        private bool IsAlreadyDateTimeStamp(DateTime date)
        {
            bool result = false;
            var data = _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Get(x => x.DateTimeStamp.Value.Date == date.Date).FirstOrDefault();
            if (data != null)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Insert Tasklist to database.
        /// </summary>
        /// <param name="taskList">The Tasklist information.</param>
        private void SaveTaskList(List<TaskListModel> taskList)
        {
            foreach (var task in taskList)
            {
                var data = new Data.Pocos.TaskList
                {
                    TimeSheetId = task.TimeSheetId,
                    ProjectCode = task.ProjectCode,
                    TypeCode = task.TypeCode,
                    Description = task.Description,
                    WorkingHours = task.WorkingHours
                };
                _unitOfWork.GetRepository<Data.Pocos.TaskList>().Add(data);
            }
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Update TimeSheet and tasklist information to database.
        /// </summary>
        /// <param name="timeSheetList">The information of timesheet and tasklist.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        /// <returns></returns>
        public ResultViewModel Update(TimeSheetViewModel formData, string email)
        {
            var result = new ResultViewModel();
            using (TransactionScope scope = new TransactionScope())
            {
                this.UpdateTimeSheet(formData.TimeSheet, email);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// Update TimeSheet to database.
        /// </summary>
        /// <param name="timeSheetList">The TimeSheet information.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        private void UpdateTimeSheet(List<TimeSheetModel> timeSheetList, string email)
        {
            foreach (var item in timeSheetList)
            {
                var data = new TimeSheet.Data.Pocos.TimeSheet
                {
                    Id = item.TimeSheetId,
                    Email = email,
                    DateTimeStamp = ConvertToDateTime(item.DateTimeStamp)
                };

                _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Update(data);
                _unitOfWork.Complete();

                item.TaskList = item.TaskList.Select(c => { c.TimeSheetId = data.Id; return c; }).ToList();
                this.UpdateTaskList(item.TimeSheetId, item.TaskList);
            }
        }

        /// <summary>
        /// Update Tasklist to database.
        /// </summary>
        /// <param name="timeSheetId">The TimeSheet id.</param>
        /// <param name="taskList">The Tasklist information.</param>
        private void UpdateTaskList(int timeSheetId, List<TaskListModel> taskList)
        {
            var data = _unitOfWork.GetRepository<Data.Pocos.TaskList>().Get(x => x.TimeSheetId == timeSheetId).ToList();

            var taskAdd = taskList.Where(x => !data.Any(y => y.Id == x.Id)).ToList();
            var taskDelete = data.Where(x => !taskList.Any(y => y.Id == x.Id)).ToList();
            data = data.Where(x => taskList.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in data)
            {
                var task = taskList.FirstOrDefault(x => x.Id == item.Id);
                if (task != null)
                {
                    item.ProjectCode = task.ProjectCode;
                    item.TypeCode = task.TypeCode;
                    item.Description = task.Description;
                    item.WorkingHours = task.WorkingHours;
                }
            }

            _unitOfWork.GetRepository<Data.Pocos.TaskList>().UpdateRange(data);
            _unitOfWork.Complete();

            this.SaveTaskList(taskAdd);
            this.DeleteTaskList(taskDelete);

        }

        /// <summary>
        /// Delete TimeSheet and Tasklist.
        /// </summary>
        /// <param name="formData">The TimeSheet and Tasklist information.</param>
        /// <returns></returns>
        public ResultViewModel Delete(TimeSheetViewModel formData)
        {
            var result = new ResultViewModel();
            using (TransactionScope scope = new TransactionScope())
            {
                this.DeleteTimeSheet(formData.TimeSheet);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// Delete TimeSheet.
        /// </summary>
        /// <param name="timeSheetList">The TimeSheet information.</param>
        private void DeleteTimeSheet(List<TimeSheetModel> timeSheetList)
        {
            foreach (var timeSheet in timeSheetList)
            {
                var dataTimeSheet = _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Get(x => x.Id == timeSheet.TimeSheetId).FirstOrDefault();
                var dataTaskList = _unitOfWork.GetRepository<Data.Pocos.TaskList>().Get(x => x.TimeSheetId == timeSheet.TimeSheetId).ToList();

                _unitOfWork.GetRepository<Data.Pocos.TimeSheet>().Remove(dataTimeSheet);
                _unitOfWork.Complete();

                this.DeleteTaskList(dataTaskList);

            }
        }

        /// <summary>
        /// Delete Tasklist.
        /// </summary>
        /// <param name="taskList">The Tasklist information.</param>
        private void DeleteTaskList(List<Data.Pocos.TaskList> taskList)
        {
            _unitOfWork.GetRepository<Data.Pocos.TaskList>().RemoveRange(taskList);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Validate date null or empty and regex is match or not.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns></returns>
        public bool IsDateMatchRegex(string date)
        {
            bool result = true;
            if (string.IsNullOrEmpty(date) || !Regex.IsMatch(date, ConstantValue.REGEX_DATE_FORMAT))
            {
                result = false;
            }
            return result;
        }

        #endregion

    }
}

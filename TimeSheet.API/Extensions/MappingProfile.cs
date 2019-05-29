using AutoMapper;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Pocos;

namespace TimeSheet.API.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TimeSheet.Data.Pocos.TimeSheet, TimeSheetViewModel>();
            CreateMap<TimeSheetViewModel, TimeSheet.Data.Pocos.TimeSheet>();
            CreateMap<TaskList, TaskListModel>();
            CreateMap<TaskListModel, TaskList>();
            CreateMap<ValueHelp, ValueHelpViewModel>();
            CreateMap<ValueHelpViewModel, ValueHelp>();
        }
    }
}
